using UnityEngine;
namespace Slimecing.StateMachine
{
    public abstract class StateChangerCondition : MonoBehaviour
    {
        public abstract bool IsMet();
    }
}
