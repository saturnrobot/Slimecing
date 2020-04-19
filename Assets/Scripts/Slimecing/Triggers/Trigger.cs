using System;
using UnityEngine;

namespace Slimecing.Triggers
{
    public abstract class Trigger : ScriptableObject
    {
        public TriggerState currentTriggerState { get; set; }
        
        public event Action<TriggerState> TriggerStateChange;
        public abstract void EnableTrigger(GameObject target);

        protected static T CheckRequestedValue<T>(object obj)
        {
            var result = default(T);
            if (obj is T)
            {
                result = (T) Convert.ChangeType(obj, typeof(T));
            }
            return result;
        }
        public virtual T ReadCurrentValue<T>() => CheckRequestedValue<T>(currentTriggerState);
        protected virtual void OnTriggerStateChange(TriggerState state)
        {
            TriggerStateChange?.Invoke(state);
        }
    }
}
