using System;
using UnityEngine;

namespace Slimecing.Environment.Moving
{
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody objectRigidbody;

        [SerializeField] private MovingController moveController;

        public Rigidbody moverRigidbody
        {
            get => objectRigidbody;
            set => objectRigidbody = value;
        }

        public Vector3 beginningTickPosition { get; set; }
        public Quaternion beginningTickRotation { get; set; }
        
        public Transform objectTransform { get; private set; }
        
        public Vector3 beginningSimulationPosition { get; private set; }
        public Quaternion beginningSimulationRotation { get; private set; }

        private Vector3 _internalCurrentPosition;

        public Vector3 currentPosition
        {
            get => _internalCurrentPosition;
            private set => _internalCurrentPosition = value;
        }

        private Quaternion _internalCurrentRotation;
        public Quaternion currentRotation
        {
            get => _internalCurrentRotation;
            private set => _internalCurrentRotation = value;
        }

        private void OnEnable()
        {
            MoverSimulationManager.CheckAlive();
            MoverSimulationManager.RegisterMover(this);
        }

        private void OnDisable()
        {
            MoverSimulationManager.UnregisterMover(this);
        }

        public void ValidateRigidbody()
        {
            objectRigidbody.centerOfMass = Vector3.zero;
            objectRigidbody.useGravity = false;
            objectRigidbody.drag = 0f;
            objectRigidbody.angularDrag = 0f;
            objectRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            objectRigidbody.isKinematic = true;
            objectRigidbody.constraints = RigidbodyConstraints.None;
            objectRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void Awake()
        {
            objectTransform = transform;
            ValidateRigidbody();
            
            moveController.InitializeMover(this);

            currentPosition = objectRigidbody.position;
            currentRotation = objectRigidbody.rotation;
            beginningSimulationPosition = objectRigidbody.position;
            beginningSimulationRotation = objectRigidbody.rotation;
        }

        public void UpdateMoverVelocity(float deltaTime)
        {
            beginningSimulationPosition = currentPosition;
            beginningSimulationRotation = currentRotation;
            
            moveController.TickMover(out _internalCurrentPosition, out _internalCurrentRotation, deltaTime);

            if (!(deltaTime > 0f)) return;
            
            objectRigidbody.velocity = (currentPosition - beginningSimulationPosition) / deltaTime;

            Quaternion rotationCurrentToTarget = currentRotation * Quaternion.Inverse(beginningSimulationRotation);
            objectRigidbody.angularVelocity = Mathf.Deg2Rad * rotationCurrentToTarget.eulerAngles / deltaTime;
        }
    }
}
