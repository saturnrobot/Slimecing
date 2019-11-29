using Slimecing.Events;
using Slimecing.SOEventSystem.Events;
using Slimecing.SOEventSystem.UnityEvents;
using UnityEngine;

namespace Slimecing.SOEventSystem.Listeners
{
    public class TriggerListener : BaseGameEventListener<Collider, TriggerEvent, UnityTriggerEvent> { }
}