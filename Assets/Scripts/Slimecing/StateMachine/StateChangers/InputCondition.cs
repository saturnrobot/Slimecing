<<<<<<< HEAD
﻿using Slimecing.Character;
using Slimecing.Characters;
=======
﻿using Slimecing.Characters;
>>>>>>> Added triggers and lots of backend
using UnityEngine;
using UnityEngine.Serialization;

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
