using System;
using UnityEngine;

namespace Slimecing.Triggers
{
    public class Trigger : ScriptableObject
    {
        public TriggerState currentTriggerState { get; set; }
        
        public event Action<InputTriggerPackage> TriggerStateChange;

        protected virtual void OnTriggerStateChange(InputTriggerPackage itp)
        {
            TriggerStateChange?.Invoke(itp);
        }
    }
}
