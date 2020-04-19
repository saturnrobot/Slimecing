using UnityEngine;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "HoldButtonInputTrigger", menuName = "Triggers/InputTriggers/HoldButtonInputTrigger")]
    public class HoldButtonInputTrigger : TriggerInput
    {
        protected override void TriggerStarted()
        {
            OnTriggerStateChange(TriggerState.Started);
        }

        protected override void TriggerPerformed()
        {
            OnTriggerStateChange(TriggerState.Performed);
        }

        protected override void TriggerCanceled()
        {
            OnTriggerStateChange(TriggerState.Canceled);
        }
    }
}
