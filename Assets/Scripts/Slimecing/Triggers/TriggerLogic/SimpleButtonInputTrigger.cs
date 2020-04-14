using Slimecing.Abilities;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "SimpleButtonInputTrigger", menuName = "Abilities/AbilityTriggers/SimpleButtonInputTrigger")]
    public class SimpleButtonInputTrigger : TriggerInput
    {
        protected override void TriggerStarted(GameObject player)
        {
            currentTriggerState = TriggerState.Performed;
            OnTriggerStateChange(new InputTriggerPackage(TriggerState.Performed, player));
        }

        protected override void TriggerPerformed(GameObject player) { }

        protected override void TriggerCanceled(GameObject player)
        {
            currentTriggerState = TriggerState.Canceled;
            OnTriggerStateChange(new InputTriggerPackage(TriggerState.Canceled, player));
        }
    }
}
