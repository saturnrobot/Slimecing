using Slimecing.SOEventSystem.Events;
using UnityEngine;

namespace Slimecing.TestScripts
{
    public class OrbitalInputChangerSwitch : MonoBehaviour
    {
        [SerializeField] private GameObjectEvent onCollisionEvent;

        private void OnCollisionEnter(Collision other)
        {
            if (other.ColIsPlayer())
            {
                onCollisionEvent.Raise(other.gameObject);
            }
        }
    }
}
