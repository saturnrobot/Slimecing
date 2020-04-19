using UnityEngine;
using Slimecing.Characters;

namespace Slimecing.Abilities {
    [System.Serializable]
    public class AbilityPackage
    {
        public Ability ability;
        [HideInInspector] public bool initialized;

        public AbilityPackage(Ability ability)
        {
            this.ability = ability;
        }

        public void Initialize(AbilityUser aUser)
        {
            if (ReferenceEquals(ability, null)) return;
            ability.Initialize(aUser);
            initialized = true;
        }
    }
}
    