using UnityEngine;

namespace Slimecing.SimpleComponents.Movement.Rotatable_Types
{
    public abstract class RotatableLogic : ScriptableObject
    {
        [SerializeField] protected float rotSpeed;
        protected bool stoppedAngVel;
        public abstract void RotateToVector(Transform objectBody, Vector3 desiredLookAt, float deltaTime);
        
        public void StopAngularVelocity(Rigidbody rb)
        {
            if (rb == null) return;
            rb.angularVelocity = Vector3.zero;
            stoppedAngVel = true;
        }

    }
}
