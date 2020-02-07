using System;
using Slimecing.SimpleComponents.Movement.Rotatable_Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace Slimecing.SimpleComponents.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class RbRotatable : MonoBehaviour
    {

        [SerializeField] private RotatableLogic rotatableLogic;
        private RotatableLogic currentRotatableLogic;

        private void Awake()
        {
            InitializeRotatableLogic();
        }

        private void InitializeRotatableLogic()
        {
            if (rotatableLogic == null) return;
            Debug.Log("Stating Rotatable Logic");
            currentRotatableLogic = rotatableLogic;
            rotatableLogic.Initialize(GetComponent<Rigidbody>(), transform);
        }

        public void RotateToVector(Vector3 desiredLookAt)
        {
            if (rotatableLogic == null) return;
            if (rotatableLogic != currentRotatableLogic) InitializeRotatableLogic();
            rotatableLogic.RotateToVector(desiredLookAt);
        }
    }
}
