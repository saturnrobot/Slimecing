using System;
using Slimecing.SimpleComponents.Movement.Rotatable_Types;
using UnityEngine;
using UnityEngine.Serialization;

namespace Slimecing.SimpleComponents.Movement
{
    public class Rotatable : MonoBehaviour
    {

        [SerializeField] private RotatableLogic rotatableLogic;
        private RotatableLogic _currentRotatableLogic;

        private void Awake()
        {
            InitializeRotatableLogic();
        }

        private void InitializeRotatableLogic()
        {
            if (rotatableLogic == null) return;
            _currentRotatableLogic = rotatableLogic;
            
            if (GetComponent<Rigidbody>() != null)
            {
                rotatableLogic.Initialize(GetComponent<Rigidbody>(), transform);
            }
            else
            {
                rotatableLogic.Initialize(transform);
            }
        }

        public void RotateToVector(Vector3 desiredLookAt, float deltaTime)
        {
            if (rotatableLogic == null) return;
            if (rotatableLogic != _currentRotatableLogic) InitializeRotatableLogic();
            rotatableLogic.RotateToVector(desiredLookAt, deltaTime);
        }
    }
}
