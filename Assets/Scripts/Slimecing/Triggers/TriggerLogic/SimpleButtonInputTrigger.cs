using UnityEngine;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "SimpleButtonInputTrigger", menuName = "Triggers/InputTriggers/SimpleButtonInputTrigger")]
    public class SimpleButtonInputTrigger : TriggerInput
    {
        protected override void TriggerStarted()
        {
            OnTriggerStateChange(TriggerState.Performed);
        }

        protected override void TriggerPerformed() { }

        protected override void TriggerCanceled()
        {
            OnTriggerStateChange(TriggerState.Canceled);
        }
    }
}
