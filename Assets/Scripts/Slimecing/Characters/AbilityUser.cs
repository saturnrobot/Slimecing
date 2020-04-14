using System.Collections.Generic;
using System.Linq;
using Slimecing.Abilities;
using Slimecing.Abilities.AbilityEffects;
using Slimecing.Abilities.UseAbilities;
using UnityEngine;

namespace Slimecing.Characters
{
    //LOOKING LIKE THERE IS TOO MANY FUNCTIONS LOOK INTO SPLITTING UP. Maybe needs interface ICooldownUser, IAbilityEffectUser
    public class AbilityUser : MonoBehaviour
    {
        //[SerializeField] private CharacterMovementController controller;

        //public CharacterMovementController Controller { get { return controller; } }
        [SerializeField] private List<AbilityPackage> useableAbilities = new List<AbilityPackage>();

        public List<TimedAbilityEffect> currentTimedAbilityEffects { get; set; } = new List<TimedAbilityEffect>();

        public List<CooldownData> abilitiesOnCooldown { get; set; } = new List<CooldownData>();

        public List<AbilityEffect> currentAbilityEffects { get; set; } = new List<AbilityEffect>();

        public AudioSource Audio { get; private set; }

        private void Awake()
        {
            Audio = GetComponent<AudioSource>();
            //Repeated code needs to be fixed!
            if (useableAbilities.Count != 0)
            {
                for (int i = useableAbilities.Count - 1; i >= 0; i--)
                {
                    if (useableAbilities[i] == null)
                    {
                        useableAbilities.RemoveAt(i);
                    }
                    else
                    {
                        useableAbilities[i].Initialize(this);
                    }
                }
            }
        }
        private void Update()
        {
            CheckAbilitiesOnCooldown();
            CheckAbilityEffects();
            CheckTimedAbilityEffects();
        }

        public bool CheckForEffects()
        {
            return currentAbilityEffects.Count != 0 || currentTimedAbilityEffects.Count != 0;
        }
        
        private void CheckTimedAbilityEffects()
        {
            for (int i = currentTimedAbilityEffects.Count - 1; i >= 0; i--)
            {
                currentTimedAbilityEffects[i].Tick(Time.deltaTime);
                if (currentTimedAbilityEffects[i].IsDone)
                {
                    RemoveTimedAbilityEffect(currentTimedAbilityEffects[i]);
                }
            }
        }
        
        private void CheckAbilityEffects()
        {
            for (int i = currentAbilityEffects.Count - 1; i >= 0; i--)
            {
                if (currentAbilityEffects[i].DoesUpdate())
                {
                    currentAbilityEffects[i].DoUpdate();
                }
            }
        }
        
        private void CheckAbilitiesOnCooldown()
        {
            //COPY PASTED CODE FROM CHECKTIMEDABILITY EFFECTS! SINGLE RESPONSIBLILITY
            for (int i = abilitiesOnCooldown.Count - 1; i >= 0; i--)
            {
                abilitiesOnCooldown[i].TickCooldown(Time.deltaTime);
                if (!abilitiesOnCooldown[i].IsCoolin)
                {
                    abilitiesOnCooldown.RemoveAt(i);
                }
            }
        }

        //Something feels wrong about this there might be a better way. Put in ability processor (struct)
        private void CheckAbility(AbilityPackage abilityPack)
        {
            Ability ability = abilityPack.ability;
            if (ReferenceEquals(ability, null))
            {
                //Make a void factory that dishes out void objects/ classes and stuff
                //it is slow to create an instance of an object ~13ms maybe make an IAbility and make a void that
                AddAbility(ScriptableObject.CreateInstance<VoidAbility>());
            }
            else if (!abilityPack.initialized)
            {
                abilityPack.Initialize(this);
            }
        }

        //CHANGE THESE THREE METHODS TO SCRIPTIBLEOBJECT EVENTS
        public void AddTimedAbilityEffect(TimedAbilityEffect abilityEffect)
        {
            currentTimedAbilityEffects.Add(abilityEffect);
            abilityEffect.Activate();
        }

        public void RemoveTimedAbilityEffect(TimedAbilityEffect abilityEffect)
        {
            currentTimedAbilityEffects.Remove(abilityEffect);
        }

        public void AddAbilityEffect(AbilityEffect abilityEffect)
        {
            currentAbilityEffects.Add(abilityEffect);
            abilityEffect.Activate();
        }
        
        public void RemoveAbilityEffect(AbilityEffect abilityEffect)
        {
            currentAbilityEffects.Remove(abilityEffect);
        }

        public void AddAbilityOnCooldown(CooldownData cooldownData)
        {
            abilitiesOnCooldown.Add(cooldownData);
        }
        
        public AbilityEffect GetAbilityEffect(AbilityEffect abilityEffect)
        {
            return currentAbilityEffects.FirstOrDefault(effect => effect.Equals(abilityEffect));
        }
        
        public TimedAbilityEffect GetTimedAbilityEffect(TimedAbilityEffect timedAbilityEffect)
        {
            return currentTimedAbilityEffects.FirstOrDefault(effect => effect.Equals(timedAbilityEffect));
        }
        
        public void AddAbility(Ability ability)
        {
            foreach (var abilities in useableAbilities)
            {
                if (!ReferenceEquals(abilities.ability, null)) continue;
                abilities.ability = ability;
                abilities.Initialize(this);
                return;
            }
            AbilityPackage abilityPack = new AbilityPackage(ability);
            useableAbilities.Add(abilityPack);
            abilityPack.Initialize(this);
        }
    }
}
