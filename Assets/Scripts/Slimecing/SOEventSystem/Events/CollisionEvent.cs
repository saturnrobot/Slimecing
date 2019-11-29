using Slimecing.Events;
using UnityEngine;

namespace Slimecing.SOEventSystem.Events
{
    [CreateAssetMenu (fileName = "New Collision Event", menuName = "Game Events/Collision Event")]
    public class CollisionEvent : BaseGameEvent<Collision> { }
}