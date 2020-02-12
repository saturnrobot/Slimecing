using UnityEngine;

namespace Slimecing.SimpleComponents.Movement.Rotatable_Types
{
    [CreateAssetMenu(fileName = "TorqueRotater", menuName = "Movement/Rotation/TorqueRotater")]
    public class TorqueRotatableLogicSO : RotatableLogic
    {
        [SerializeField] protected float alignmentDamping;
        [SerializeField] private float maxAngularVelocity;
        
        private bool _slowedAngVel;

        public override void Initialize(Rigidbody rbody, Transform body)
        {
            rb = rbody;
            objectTransform = body;
            rb.maxAngularVelocity = maxAngularVelocity;
        }
        
        public override void RotateToVector(Vector3 desiredLookAt, float deltaTime)
        {
            if (desiredLookAt == Vector3.zero || !(rotSpeed > 0f)) return;
            Quaternion targetRotation = Quaternion.LookRotation(desiredLookAt);
            
            if (Quaternion.Angle(objectTransform.rotation, targetRotation) < 13)
            {
                if (!_slowedAngVel)
                {
                    SlowAngularVelocity();
                }
                objectTransform.rotation = Quaternion.Slerp(objectTransform.rotation, targetRotation, rotSpeed);
                return;
            }

            if (_slowedAngVel)
                _slowedAngVel = false;
            if (stoppedAngVel)
                stoppedAngVel = false;
            
            Quaternion deltaRotation = Quaternion.Inverse(objectTransform.rotation) * targetRotation;
            Vector3 deltaAngles = GetRelativeAngles(deltaRotation.eulerAngles);
            Vector3 spaceDeltaAngles = objectTransform.TransformDirection(deltaAngles);
            Vector3 rotationVelocity = rotSpeed * Time.deltaTime * spaceDeltaAngles;
            rb.AddTorque(rotationVelocity - rb.angularVelocity, ForceMode.Impulse);
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

        private void SlowAngularVelocity()
        {
            rb.angularVelocity *= 0.5f;
            _slowedAngVel = true;
        }
    }
}
