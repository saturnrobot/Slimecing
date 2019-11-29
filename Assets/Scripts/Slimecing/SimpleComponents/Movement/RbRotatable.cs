using System;
using UnityEngine;

namespace Slimecing.SimpleComponents.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RbRotatable : MonoBehaviour
    {
        [SerializeField] protected float rotSpeed;
        [SerializeField] protected float alignmentDamping;
        [SerializeField] private float maxAngularVelocity;

        private bool _slowedAngVel;
        private bool _stoppedAngVel;
        private Rigidbody rb;

        public bool StoppedAngVel => _stoppedAngVel; 

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.maxAngularVelocity = maxAngularVelocity;
        }

        public void RotateToVector(Vector3 desiredLookAt)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desiredLookAt);
            
            if (Quaternion.Angle(transform.rotation, targetRotation) < 13)
            {
                if (!_slowedAngVel)
                {
                    SlowAngularVelocity();
                }
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed);
                return;
            }

            if (_slowedAngVel)
                _slowedAngVel = false;
            if (_stoppedAngVel)
                _stoppedAngVel = false;
            
            Quaternion deltaRotation = Quaternion.Inverse(transform.rotation) * targetRotation;
            Vector3 deltaAngles = GetRelativeAngles(deltaRotation.eulerAngles);
            Vector3 spaceDeltaAngles = transform.TransformDirection(deltaAngles);
            rb.AddTorque(rotSpeed * spaceDeltaAngles - alignmentDamping * rb.angularVelocity);
        }

        private Vector3 GetRelativeAngles(Vector3 angles)
        {
            Vector3 relativeAngles = angles;
            if (relativeAngles.x > 180f)
                relativeAngles.x -= 360f;
            if (relativeAngles.y > 180f)
                relativeAngles.y -= 360f;
            if (relativeAngles.z > 180f)
                relativeAngles.z -= 360f;
 
            return relativeAngles;
        }

        public void SlowAngularVelocity()
        {
            rb.angularVelocity *= 0.5f;
            _slowedAngVel = true;
        }

        public void StopAngularVelocity()
        {
            rb.angularVelocity = Vector3.zero;
            _stoppedAngVel = true;
        }
    
    }
}
