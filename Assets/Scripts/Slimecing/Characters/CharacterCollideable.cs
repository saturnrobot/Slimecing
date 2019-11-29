using System;
using Slimecing.SOEventSystem.Events;
using UnityEngine;

namespace Slimecing.Characters
{
    public class CharacterCollideable : MonoBehaviour
    {
        [Header("Events")] 
        [SerializeField] private CollisionEvent onSlimeColliderEnter;
        private void OnCollisionEnter(Collision other)
        {
            //Debug.Log(other.gameObject.tag);
            switch (other.gameObject.tag)
            {
                case "Slime":
                    onSlimeColliderEnter.Raise(other);
                    break;
            }
        }

        /*private void OnCollisionExit(Collision other)
        {
            throw new NotImplementedException();
        }*/
    }
}
