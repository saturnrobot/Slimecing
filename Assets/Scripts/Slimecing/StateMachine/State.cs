using System.Collections.Generic;
using UnityEngine;

namespace Slimecing.StateMachine
{
    public class State : MonoBehaviour, IState
    {
        [SerializeField] private List<StateChanger> changes = new List<StateChanger>();
        
        public IState CheckTransition()
        {
            foreach (var change in changes)
            {
                if (change.ShouldChange())
                {
                    return change.NextState;
                }
            }

            return null;
        }

        public void Enter() => gameObject.SetActive(true);

        public void Exit() => gameObject.SetActive(false);
    }
}
