using UnityEngine;

namespace Slimecing.Swords.DropBehaviour.DropBehaviours
{
    public class RigidbodyDropLogic : DropLogic
    {
        [SerializeField] private Rigidbody rigidbody;
        
        public override void Drop()
        {
            if (rigidbody.isKinematic)
            {
                rigidbody.isKinematic = false;
            }

            if (!rigidbody.useGravity)
            {
                rigidbody.useGravity = true;
            }

            rigidbody.constraints = RigidbodyConstraints.None;

            rigidbody.velocity = Vector3.zero;
        }
    }
}
