using Slimecing.Events;
using Slimecing.SOEventSystem.Events;
using UnityEngine;
using UnityEngine.Events;

namespace Slimecing.SOEventSystem.Listeners
{
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent;
        public E GameEvent { get { return gameEvent; } set {gameEvent = value;} }

        [SerializeField] private UER unityEventResponse;

        private void OnEnable()
        {
            if(gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if(gameEvent == null) { return; }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T thing)
        {
            unityEventResponse?.Invoke(thing);
        }
    }
}