using System;
using UnityEngine;

namespace Slimecing.Environment.Moving.MovingPlatformLogic
{
    public class BasicMovingPlatform : MovingController
    {
        [SerializeField] private Vector3 translationAxis = Vector3.right;
        [SerializeField] private float translationPeriod = 10;
        [SerializeField] private float translationSpeed = 1;
        [SerializeField] private Vector3 rotationAxis = Vector3.up;
        [SerializeField] private float rotSpeed = 10;
        [SerializeField] private Vector3 oscillationAxis = Vector3.zero;
        [SerializeField] private float oscillationPeriod = 10;
        [SerializeField] private float oscillationSpeed = 10;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        private void Start()
        {
            _originalPosition = mover.moverRigidbody.position;
            _originalRotation = mover.moverRigidbody.rotation;
        }

        public override void TickMover(out Vector3 targetPosition, out Quaternion targetRotation, float deltaTime)
        {
            targetPosition = _originalPosition +
                             translationAxis.normalized * (Mathf.Sin(Time.time * translationSpeed) * translationPeriod);
            Quaternion targetRotForOsc = Quaternion.Euler(oscillationAxis.normalized * (Mathf.Sin(Time.time * oscillationSpeed) * oscillationPeriod)) * _originalRotation;
            targetRotation = Quaternion.Euler(rotationAxis * (rotSpeed * Time.time)) * targetRotForOsc;
        }
    }
}
