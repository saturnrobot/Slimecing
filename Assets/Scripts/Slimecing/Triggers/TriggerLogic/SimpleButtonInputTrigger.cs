using Slimecing.Abilities;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "SimpleButtonInputTrigger", menuName = "Triggers/InputTriggers/SimpleButtonInputTrigger")]
    public class SimpleButtonInputTrigger : TriggerInput
    {
        protected override void TriggerStarted(GameObject player)
        {
            currentTriggerState = TriggerState.Performed;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Performed, player));
        }

        protected override void TriggerPerformed(GameObject player) { }

        protected override void TriggerCanceled(GameObject player)
        {
            currentTriggerState = TriggerState.Canceled;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Canceled, player));
        }
    }
}
