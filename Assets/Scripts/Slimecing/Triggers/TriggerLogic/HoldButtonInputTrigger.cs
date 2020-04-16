using Slimecing.Abilities;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "HoldButtonInputTrigger", menuName = "Triggers/InputTriggers/HoldButtonInputTrigger")]
    public class HoldButtonInputTrigger : TriggerInput
    {
        protected override void TriggerStarted(GameObject player)
        {
            currentTriggerState = TriggerState.Started;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Started, player));
        }

        protected override void TriggerPerformed(GameObject player)
        {
            currentTriggerState = TriggerState.Performed;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Performed, player));
        }

        protected override void TriggerCanceled(GameObject player)
        {
             currentTriggerState = TriggerState.Canceled;
             OnTriggerStateChange(new TriggerPackage(TriggerState.Canceled, player));
        }
    }
}
