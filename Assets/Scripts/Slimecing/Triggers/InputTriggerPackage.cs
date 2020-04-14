using UnityEngine;

namespace Slimecing.Triggers
{
    public struct InputTriggerPackage
    {
        public TriggerState triggerState { get; set; }
        public GameObject user { get; set; }

        public InputTriggerPackage(TriggerState triggerState, GameObject user)
        {
            this.triggerState = triggerState;
            this.user = user;
        }
    }
}
