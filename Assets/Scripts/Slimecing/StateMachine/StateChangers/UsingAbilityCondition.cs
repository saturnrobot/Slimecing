<<<<<<< HEAD
﻿using Slimecing.Character;
=======
﻿using Slimecing.Characters;
>>>>>>> Added triggers and lots of backend
using UnityEngine;

namespace Slimecing.StateMachine.StateChangers
{
    public class UsingAbilityCondition : StateChangerCondition
    {
        [SerializeField] private AbilityUser abilityUser;
        [SerializeField] private bool isUsingAbility;
        
        public override bool IsMet()
        {
<<<<<<< HEAD
=======
            if (abilityUser == null) return false;
>>>>>>> Added triggers and lots of backend
            return abilityUser.CheckForEffects() == isUsingAbility;
        }
    }
}
