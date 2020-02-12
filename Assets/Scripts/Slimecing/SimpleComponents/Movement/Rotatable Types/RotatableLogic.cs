using UnityEngine;

namespace Slimecing.SimpleComponents.Movement.Rotatable_Types
{
    public abstract class RotatableLogic : ScriptableObject
    {
        [SerializeField] protected float rotSpeed;
        protected Rigidbody rb;
        protected Transform objectTransform;
        protected bool stoppedAngVel;

       public virtual void Initialize(Rigidbody rbody, Transform body)
       {
            rb = rbody;
            objectTransform = body;
       }

       public virtual void Initialize(Transform body)
       {
           objectTransform = body;
       }
        
        public abstract void RotateToVector(Vector3 desiredLookAt, float deltaTime);
        
        public void StopAngularVelocity()
        {
            if (rb == null) return;
            rb.angularVelocity = Vector3.zero;
            stoppedAngVel = true;
        }

    }
}
