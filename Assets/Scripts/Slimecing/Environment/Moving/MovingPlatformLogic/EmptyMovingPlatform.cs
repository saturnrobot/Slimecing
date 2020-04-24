using UnityEngine;

namespace Slimecing.Environment.Moving.MovingPlatformLogic
{
    public class EmptyMovingPlatform : MovingController
    {
        public override void TickMover(out Vector3 targetPosition, out Quaternion targetRotation, float deltaTime)
        {
            var moverTransform = transform;
            targetPosition = mover.moverRigidbody.position;
            targetRotation = mover.moverRigidbody.rotation;
        }
    }
}
