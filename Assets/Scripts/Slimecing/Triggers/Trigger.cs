using System;
using UnityEngine;

namespace Slimecing.Triggers
{
    public abstract class Trigger : ScriptableObject
    {
        public TriggerState currentTriggerState { get; set; }
        
        public event Action<TriggerState> TriggerStateChange;

        public Trigger GetTrigger() => Instantiate(this);
        public abstract void EnableTrigger(GameObject target);
        public virtual T ReadCurrentValue<T>() => (T) Convert.ChangeType(currentTriggerState, typeof(T));
        protected void OnTriggerStateChange(TriggerState state)
        {
            TriggerStateChange?.Invoke(state);
        }
    }
}
