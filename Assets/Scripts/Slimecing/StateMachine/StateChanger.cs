using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slimecing.StateMachine
{
    [Serializable]
    public class StateChanger
    {
        [SerializeField] private State nextState = null;
        [SerializeField] private List<StateChangerCondition> conditions = new List<StateChangerCondition>();

        public State NextState => nextState;

        public bool ShouldChange()
        {
            foreach (var condition in conditions)
            {
                if (!condition.IsMet())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
