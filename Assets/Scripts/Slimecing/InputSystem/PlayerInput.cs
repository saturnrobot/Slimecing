using System;
using Slimecing.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.InputSystem
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class PlayerInput : MonoBehaviour, SlimeControls.IVSGameplayActions
    {
        [SerializeField] private SlimeControls playerControls;

        private CharacterMovementController characterMovementController;
        
        private void Awake()
        {
            playerControls = new SlimeControls();
            playerControls.VSGameplay.SetCallbacks(this);
            characterMovementController = GetComponent<CharacterMovementController>();
        }
        
        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        public void OnHorizontal(InputAction.CallbackContext context)
        {
            characterMovementController.GetMoveInputH(context.ReadValue<float>());
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            characterMovementController.GetMoveInputV(context.ReadValue<float>());
        }
    }
}
