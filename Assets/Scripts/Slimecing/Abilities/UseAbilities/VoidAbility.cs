﻿using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities.UseAbilities
{
    public class VoidAbility : Ability
    {
        public override void Initialize(AbilityUser aUser)
        {
            base.Initialize(aUser);
            abilityName = "Void Ability";
            Debug.LogWarning(aUser.name + " added a void ability");
        }
        public override void Use(AbilityUser aUser) { }
    }
}
