using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Triggers
{
    public abstract class TriggerInput : Trigger
    {
        [SerializeField] private InputActionReference inputActionReference;
        public InputActionReference currentActionReference { get => inputActionReference; set => inputActionReference = value; }
        public InputAction action { get; set; }
        public override void EnableTrigger(GameObject target)
        {
            PlayerInput currentPlayerInput = target.GetComponent<PlayerInput>();
            if (currentPlayerInput == null) return;

            foreach (var a in currentPlayerInput.actions)
            {
                if (!currentActionReference.action.name.Equals(a.name)) continue;
                a.started += ctx => TriggerStarted();
                a.performed += ctx => TriggerPerformed();
                a.canceled += ctx => TriggerCanceled();
                a.Enable();
            }
        }

        public void OnDisable()
        {
            action?.Disable();
        }
        public override T ReadCurrentValue<T>()
        {
            return (T) Convert.ChangeType(action, typeof(T));
        } 
        
        protected abstract void TriggerStarted();
        protected abstract void TriggerPerformed();
        protected abstract void TriggerCanceled();
        
    }
}
