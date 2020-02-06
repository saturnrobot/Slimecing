namespace Slimecing.StateMachine
{
   public interface IState
   {  
      IState CheckTransition();
      void Enter();
      void Exit();
   }
}
