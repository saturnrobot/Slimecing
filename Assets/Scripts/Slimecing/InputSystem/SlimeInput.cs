using System;
using Slimecing.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.InputSystem
{
    public class SlimeInput : TakesInput, SlimeControls.IVsGameplayActions
    {
        [SerializeField] private CharacterMovementController _characterMovementController;
        
        public void OnHorizontal(InputAction.CallbackContext context)
        {
            if (_characterMovementController == null) return;
            _characterMovementController.GetMoveInputH(context.ReadValue<float>());
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            if (_characterMovementController == null) return;
            _characterMovementController.GetMoveInputV(context.ReadValue<float>());
        }
    }
}
