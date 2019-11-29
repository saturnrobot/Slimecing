﻿using Slimecing.Character;
namespace Slimecing.Abilities {
    [System.Serializable]
    public class AbilityPackage
    {
        public Ability ability;
        [UnityEngine.HideInInspector] public bool initialized = false;

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
    