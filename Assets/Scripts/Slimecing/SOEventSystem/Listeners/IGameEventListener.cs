namespace Slimecing.SOEventSystem.Listeners
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T thing);
    }
}