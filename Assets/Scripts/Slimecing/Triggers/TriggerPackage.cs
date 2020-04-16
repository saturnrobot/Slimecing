using UnityEngine;

namespace Slimecing.Triggers
{
    public struct TriggerPackage
    {
        public TriggerState triggerState { get; set; }
        public GameObject user { get; set; }

        public TriggerPackage(TriggerState triggerState, GameObject user)
        {
            this.triggerState = triggerState;
            this.user = user;
        }
    }
}
