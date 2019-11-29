using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slimecing.Abilities;
using System;
using Slimecing.Abilities.AbilityEffects;

namespace Slimecing.Character
{
    //LOOKING LIKE THERE IS TOO MANY FUNCTIONS LOOK INTO SPLITTING UP. Maybe needs interface ICooldownUser, IAbilityEffectUser
    public class AbilityUser : MonoBehaviour
    {
        [SerializeField] private List<AbilityPackage> useableAbilities = new List<AbilityPackage>();

        private List<TimedAbilityEffect> currentTimedAbilityEffects = new List<TimedAbilityEffect>();
        private List<CooldownData> abilitiesOnCooldown = new List<CooldownData>();
        private List<AbilityEffect> currentAbilityEffects = new List<AbilityEffect>();

        public IEnumerable<CooldownData> AbilitiesOnCooldown => abilitiesOnCooldown;

        private void Awake()
        {
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
            GetAbilityInput();
            CheckAbilitiesOnCooldown();
            CheckTimedAbilityEffects();
        }


        //USE INPUT MANAGER
        private void GetAbilityInput()
        {
            if (useableAbilities.Count == 0) return;
            foreach (var abilities in useableAbilities)
            {
                CheckAbility(abilities);
                if (Input.GetButtonDown(abilities.ability.AbilityButton))
                {
                    abilities.ability.StartAbility(this);
                }
            }
        }

        private void CheckTimedAbilityEffects()
        {
            for (int i = currentTimedAbilityEffects.Count - 1; i >= 0; i--)
            {
                currentTimedAbilityEffects[i].Tick(Time.deltaTime);
                if (currentTimedAbilityEffects[i].IsDone)
                {
                    currentTimedAbilityEffects.RemoveAt(i);
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
            if (ReferenceEquals(abilityPack.ability, null))
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

        public void AddAbilityEffect(AbilityEffect abilityEffect)
        {
            currentAbilityEffects.Add(abilityEffect);
            abilityEffect.Activate();
        }

        public void AddAbilityOnCooldown(CooldownData cooldownData)
        {
            abilitiesOnCooldown.Add(cooldownData);
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
