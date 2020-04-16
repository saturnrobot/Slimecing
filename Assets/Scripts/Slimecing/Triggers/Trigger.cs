using System;
using UnityEngine;

namespace Slimecing.Triggers
{
    public class Trigger : ScriptableObject
    {
        public TriggerState currentTriggerState { get; set; }
        
        public event Action<TriggerPackage> TriggerStateChange;

        protected virtual void OnTriggerStateChange(TriggerPackage itp)
        {
            TriggerStateChange?.Invoke(itp);
        }
    }
}
