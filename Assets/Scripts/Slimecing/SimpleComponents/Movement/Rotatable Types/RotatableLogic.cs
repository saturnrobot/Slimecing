using UnityEngine;

namespace Slimecing.SimpleComponents.Movement.Rotatable_Types
{
    public abstract class RotatableLogic : ScriptableObject
    {
        [SerializeField] protected float rotSpeed;
<<<<<<< HEAD
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
=======
        protected bool stoppedAngVel;
        public abstract void RotateToVector(Transform objectBody, Vector3 desiredLookAt, float deltaTime);
        
        public void StopAngularVelocity(Rigidbody rb)
>>>>>>> Added triggers and lots of backend
        {
            if (rb == null) return;
            rb.angularVelocity = Vector3.zero;
            stoppedAngVel = true;
        }

    }
}
