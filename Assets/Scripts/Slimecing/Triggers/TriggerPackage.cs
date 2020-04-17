using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Triggers
{
    public struct TriggerPackage
    {

        public TriggerState triggerState { get; set; }
        public GameObject user { get; set; }
        public InputAction.CallbackContext ctx { get; set; }
        
        public TriggerPackage(TriggerState triggerState, GameObject user)
        {
            this.triggerState = triggerState;
            this.user = user;
            ctx = new InputAction.CallbackContext();
        }
        public TriggerPackage(TriggerState triggerState, GameObject user, InputAction.CallbackContext ctx)
        {
            this.triggerState = triggerState;
            this.user = user;
            this.ctx = ctx;
        }
        
    }
}
