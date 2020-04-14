using Slimecing.Abilities;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "HoldButtonInputTrigger", menuName = "Abilities/AbilityTriggers/HoldButtonInputTrigger")]
    public class HoldButtonInputTrigger : TriggerInput
    {
        protected override void TriggerStarted(GameObject player)
        {
            currentTriggerState = TriggerState.Started;
            OnTriggerStateChange(new InputTriggerPackage(TriggerState.Started, player));
        }

        protected override void TriggerPerformed(GameObject player)
        {
            currentTriggerState = TriggerState.Performed;
            OnTriggerStateChange(new InputTriggerPackage(TriggerState.Performed, player));
        }

        protected override void TriggerCanceled(GameObject player)
        {
             currentTriggerState = TriggerState.Canceled;
             OnTriggerStateChange(new InputTriggerPackage(TriggerState.Canceled, player));
        }
    }
}
