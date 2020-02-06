using UnityEngine;

namespace Slimecing.StateMachine
{
    public class StateMachineComponent : MonoBehaviour
    {
        [SerializeField] private State startingState = null;

        private StateMachine _stateMachine;

        public StateMachine StateMachine
        {
            get
            {
                if (_stateMachine != null)
                {
                    return _stateMachine;
                }
                
                _stateMachine = new StateMachine(startingState);
                return _stateMachine;
            }
        }

        private void Update() => StateMachine.Tick();

        public void ChangeState(State state) => StateMachine.ChangeState(state);
    }
}
