using UnityEngine;

namespace Slimecing.SimpleComponents.Movement.Rotatable_Types
{
    [CreateAssetMenu(fileName = "SimpleRotater", menuName = "Movement/Rotation/SimpleRotater")]
    public class SimpleRotatableLogicSO : RotatableLogic
    {
        public override void RotateToVector(Vector3 desiredLookAt, float deltaTime)
        {
            if (desiredLookAt == Vector3.zero || !(rotSpeed > 0f)) return;
            Vector3 smoothedLookAtDirection = Vector3.Slerp(objectTransform.forward, desiredLookAt,
                1 - Mathf.Exp(-rotSpeed * deltaTime)).normalized;
            objectTransform.rotation = Quaternion.LookRotation(smoothedLookAtDirection, objectTransform.up);
        }
    }
    
}
