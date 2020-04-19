using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Triggers
{
    public abstract class TriggerInput : Trigger
    {
        [SerializeField] private InputActionReference inputActionReference;
        
        public InputAction.CallbackContext inputContext { get; set; }
        
        public InputActionReference currentActionReference { get => inputActionReference; set => inputActionReference = value; }

        public PlayerInput currentPlayerInput { get; private set; }

        public override void EnableTrigger(GameObject target)
        {
            currentPlayerInput = target.GetComponent<PlayerInput>();
            if (currentPlayerInput == null) return;

            foreach (var action in currentPlayerInput.actions)
            {
                if (!currentActionReference.action.name.Equals(action.name)) continue;
                action.started += ctx => TriggerStarted(target, ctx);
                action.performed += ctx => TriggerPerformed(target, ctx);
                action.canceled += ctx => TriggerCanceled(target, ctx);
                action.Enable();
            }
        }

        public void OnDisable()
        {
            if (currentPlayerInput == null) return;
            foreach (var action in currentPlayerInput.user.actions)
            {
                if (!inputActionReference.action.name.Equals(action.name)) continue;
                action.Disable();
            }
        }
        protected abstract void TriggerStarted(GameObject player, InputAction.CallbackContext ctx);
        protected abstract void TriggerPerformed(GameObject player, InputAction.CallbackContext ctx);
        protected abstract void TriggerCanceled(GameObject player,InputAction.CallbackContext ctx);
        
    }
}
