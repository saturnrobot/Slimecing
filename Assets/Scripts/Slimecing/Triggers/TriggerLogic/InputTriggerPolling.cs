using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "AxisInputTrigger(Polling)", menuName = "Triggers/InputTriggers/AxisInputTrigger(Polling)")]
    public class InputTriggerPolling : Trigger
    {
        [SerializeField] private InputActionReference inputActionReference;

        public InputActionReference currentActionReference
        {
            get => inputActionReference;
            set => inputActionReference = value;
        }
        
        public InputAction action { get; set; }
        public PlayerInput currentPlayerInput { get; set; }
        public override void EnableTrigger(GameObject target)
        {
            currentPlayerInput = target.GetComponent<PlayerInput>();
            if (currentPlayerInput == null) return;

            foreach (var a in currentPlayerInput.actions)
            {
                if (!currentActionReference.action.name.Equals(a.name)) continue;
                a.Enable();
                action = a;
            }
            
        }

        public override T ReadCurrentValue<T>()
        {
            return (T) Convert.ChangeType(action, typeof(T));
        } 

    }
}
