using Slimecing.Events;
using UnityEngine;

namespace Slimecing.SOEventSystem.Events
{
    [CreateAssetMenu (fileName = "New Void Event", menuName = "Game Events/Void Event")]
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise()
        {
            Raise(new Void());
        }
    }
}