using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Triggers.TriggerLogic
{
    [CreateAssetMenu(fileName = "AxisInputTrigger(Polling)", menuName = "Triggers/InputTriggers/AxisInputTrigger(Polling)")]
    public class AxisInputTriggerPolling : TriggerInput
    {
        protected override void TriggerStarted(GameObject player, InputAction.CallbackContext ctx)
        {
            
        }

        protected override void TriggerPerformed(GameObject player, InputAction.CallbackContext ctx)
        {
            currentTriggerState = TriggerState.Performed;
            inputContext = ctx;
        }

        protected override void TriggerCanceled(GameObject player, InputAction.CallbackContext ctx)
        {
            currentTriggerState = TriggerState.Canceled;
            inputContext = ctx;
        }
    }
}
