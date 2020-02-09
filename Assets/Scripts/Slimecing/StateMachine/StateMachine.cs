using UnityEngine;

namespace Slimecing.StateMachine
{
    public class StateMachine
    {
        public StateMachine(IState startingState) => ChangeState(startingState);
        
        public IState CurrState { get; private set; }

        private GameObject _stateMachineTarget;

        public void ChangeState(IState state)
        {
            CurrState?.Exit();

            CurrState = state;

            CurrState?.Enter();
        }

        public void Tick()
        {
            IState nextState = CurrState.CheckTransition();

            if (nextState != null)
            {
                ChangeState(nextState);
            }
        }
    }
}
