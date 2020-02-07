using UnityEngine;

namespace Slimecing.SimpleComponents.Movement.Rotatable_Types
{
    [CreateAssetMenu(fileName = "SimpleRotater", menuName = "Movement/Rotation/SimpleRotater")]
    public class SimpleRotatableLogicSO : RotatableLogic
    {
        public override void RotateToVector(Vector3 desiredLookAt)
        {
            objectTransform.rotation = Quaternion.Slerp(objectTransform.rotation, 
                Quaternion.LookRotation(desiredLookAt), rotSpeed * Time.deltaTime);
        }
    }
}
