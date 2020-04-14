using Slimecing.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.InputSystem
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class PlayerInput : MonoBehaviour, SlimeControls.IVsGameplayActions
    {
        [SerializeField] private SlimeControls playerControls;

        private CharacterMovementController _characterMovementController;
        
        private void Awake()
        {
            playerControls = new SlimeControls();
            playerControls.VsGameplay.SetCallbacks(this);
            _characterMovementController = GetComponent<CharacterMovementController>();
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
            _characterMovementController.GetMoveInputH(context.ReadValue<float>());
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            _characterMovementController.GetMoveInputV(context.ReadValue<float>());
        }
    }
}
