using Slimecing.Events;
using UnityEngine;

namespace Slimecing.SOEventSystem.Events
{
    [CreateAssetMenu (fileName = "New GameObject Event", menuName = "Game Events/GameObject Event")]
    public class GameObjectEvent : BaseGameEvent<GameObject>{}
}