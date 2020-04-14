using Slimecing.SimpleComponents.Movement.Rotatable_Types;
using UnityEngine;

namespace Slimecing.SimpleComponents.Movement
{
    public class Rotatable : MonoBehaviour
    {

        [SerializeField] private RotatableLogic rotatableLogic;

        public void RotateToVector(Transform objectTransform, Vector3 desiredLookAt, float deltaTime)
        {
            if (rotatableLogic == null) return;
            rotatableLogic.RotateToVector(objectTransform, desiredLookAt, deltaTime);
        }
    }
}
