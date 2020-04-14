using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.StateMachine.StateChangers
{
    public class UsingAbilityCondition : StateChangerCondition
    {
        [SerializeField] private AbilityUser abilityUser;
        [SerializeField] private bool isUsingAbility;
        
        public override bool IsMet()
        {
            if (abilityUser == null) return false;
            return abilityUser.CheckForEffects() == isUsingAbility;
        }
    }
}
