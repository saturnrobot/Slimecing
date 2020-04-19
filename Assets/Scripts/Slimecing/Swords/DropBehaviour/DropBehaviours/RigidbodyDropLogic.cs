using UnityEngine;

namespace Slimecing.Swords.DropBehaviour.DropBehaviours
{
    public class RigidbodyDropLogic : DropLogic
    {
        [SerializeField] private Rigidbody rb;
        
        public override void Drop()
        {
            if (rb.isKinematic)
            {
                rb.isKinematic = false;
            }

            if (!rb.useGravity)
            {
                rb.useGravity = true;
            }

            rb.constraints = RigidbodyConstraints.None;

            rb.velocity = Vector3.zero;
        }
    }
}
