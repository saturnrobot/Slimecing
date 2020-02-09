using Slimecing.Character;
using UnityEngine;

namespace Slimecing.StateMachine.StateChangers
{
    public class UsingAbilityCondition : StateChangerCondition
    {
        [SerializeField] private AbilityUser abilityUser;
        [SerializeField] private bool isUsingAbility;
        
        public override bool IsMet()
        {
            return abilityUser.CheckForEffects() == isUsingAbility;
        }
    }
}
