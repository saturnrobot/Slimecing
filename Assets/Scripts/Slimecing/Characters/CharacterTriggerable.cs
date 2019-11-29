using System;
using Slimecing.SOEventSystem.Events;
using UnityEngine;

namespace Slimecing.Characters
{
    public class CharacterTriggerable : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private TriggerEvent onSlimeTriggerEnter;
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                case "Slime":
                    onSlimeTriggerEnter.Raise(other);
                    break;
            }
        }

        /*private void OnTriggerExit(Collider other)
        {
            onTriggerExit.Raise(other);
        }*/
    }
}
