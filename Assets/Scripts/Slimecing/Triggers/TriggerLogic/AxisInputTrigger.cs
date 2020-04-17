using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "TwoDimensionalAxisInputTrigger ", menuName = "Triggers/InputTriggers/TwoDimensionalAxisInputTrigger ")]
    public class TwoDimensionalAxisInputTrigger : TriggerInput
    {
        protected override void TriggerStarted(GameObject player, InputAction.CallbackContext ctx)
        {
            currentTriggerState = TriggerState.Performed;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Performed, player, ctx));
        }

        protected override void TriggerPerformed(GameObject player, InputAction.CallbackContext ctx)
        {
            currentTriggerState = TriggerState.Performed;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Performed, player, ctx));
        }

        protected override void TriggerCanceled(GameObject player, InputAction.CallbackContext ctx)
        {
            currentTriggerState = TriggerState.Canceled;
            OnTriggerStateChange(new TriggerPackage(TriggerState.Canceled, player, ctx));
        }
    }
}
