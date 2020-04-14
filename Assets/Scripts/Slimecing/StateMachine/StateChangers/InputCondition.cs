using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.StateMachine.StateChangers
{
    public class InputCondition : StateChangerCondition
    {
        [SerializeField] private CharacterMovementController characterMovementController;
        [SerializeField] private bool isTakingInput;
        public override bool IsMet()
        {
            return (characterMovementController.MoveInput != Vector2.zero) == isTakingInput;
        }
    }
}
