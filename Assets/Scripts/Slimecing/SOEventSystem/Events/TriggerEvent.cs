using Slimecing.Events;
using UnityEngine;

namespace Slimecing.SOEventSystem.Events
{
    [CreateAssetMenu (fileName = "New Trigger Event", menuName = "Game Events/Trigger Event")]
    public class TriggerEvent : BaseGameEvent<Collider> { }
}