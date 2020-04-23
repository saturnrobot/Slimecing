using UnityEngine;

namespace Slimecing.Environment.Moving
{
    public abstract class MovingController : MonoBehaviour
    {
        //public Vector3 originalPosition { get; set; }

        //public Quaternion originalRotation { get; set; }

        public RigidbodyMover mover { get; private set; }

        public void InitializeMover(RigidbodyMover rbMover)
        {
            mover = rbMover;
        }

        public abstract void TickMover(out Vector3 targetPosition, out Quaternion targetRotation, float deltaTime);
    }
}
