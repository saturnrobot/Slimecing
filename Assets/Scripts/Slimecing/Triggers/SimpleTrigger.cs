using UnityEngine;

namespace Slimecing.Triggers
{
    public abstract class SimpleTrigger : Trigger
    {
        public abstract void InitializeTrigger(GameObject target);
        protected abstract void TriggerPerformed(GameObject target);
    }
}
