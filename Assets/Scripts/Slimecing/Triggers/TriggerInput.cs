using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Triggers
{
    public abstract class TriggerInput : Trigger
    {
        [SerializeField] private InputActionReference inputActionReference;
        
        public InputActionReference currentActionReference { get => inputActionReference; set => inputActionReference = value; }

        public PlayerInput currentPlayerInput { get; set; }

        public void ConfigureInput(GameObject player)
        {
            currentPlayerInput = player.GetComponent<PlayerInput>();
            if (currentPlayerInput == null) return;

            foreach (var action in currentPlayerInput.actions)
            {
                if (!currentActionReference.action.name.Equals(action.name)) continue;
                action.started += ctx => TriggerStarted(player);
                action.performed += ctx => TriggerPerformed(player);
                action.canceled += ctx => TriggerCanceled(player);
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
        protected abstract void TriggerStarted(GameObject player);
        protected abstract void TriggerPerformed(GameObject player);
        protected abstract void TriggerCanceled(GameObject player);
    }
}
