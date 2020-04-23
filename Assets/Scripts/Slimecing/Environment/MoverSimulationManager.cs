using System;
using Boo.Lang;
using Slimecing.Environment.Moving;
using UnityEngine;

namespace Slimecing.Environment
{
    [DefaultExecutionOrder(-100)]
    public class MoverSimulationManager : MonoBehaviour
    {
        private static MoverSimulationManager _instance;

        private static List<RigidbodyMover> _rigidbodyMovers = new List<RigidbodyMover>();
        
        public static void CheckAlive()
        {
            if (_instance != null) return;
            
            GameObject holdingGameObject = new GameObject("MoverSimManager");
            _instance = holdingGameObject.AddComponent<MoverSimulationManager>();

            holdingGameObject.hideFlags = HideFlags.NotEditable;
            _instance.hideFlags = HideFlags.NotEditable;
        }
        private void Awake()
        {
            _instance = this;
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }
        
        public static void RegisterMover(RigidbodyMover mover)
        {
            _rigidbodyMovers.Add(mover);
        }

        public static void UnregisterMover(RigidbodyMover mover)
        {
            _rigidbodyMovers.Remove(mover);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.deltaTime;
            PreSimulationTick();
            SimulateTick(deltaTime);
            PostSimulationTick();
        }

        private void PreSimulationTick()
        {
            foreach (var mover in _rigidbodyMovers)
            {
                var moverTransform = mover.transform;
                mover.beginningTickPosition = moverTransform.position;
                mover.beginningTickRotation = moverTransform.rotation;
            }
        }

        private void SimulateTick(float deltaTime)
        {
            foreach (var mover in _rigidbodyMovers)
            {
                mover.UpdateMoverVelocity(deltaTime);

                mover.transform.SetPositionAndRotation(mover.currentPosition, mover.currentRotation);
                mover.moverRigidbody.position = mover.currentPosition;
                mover.moverRigidbody.rotation = mover.currentRotation;
            }
        }

        private void PostSimulationTick()
        {
            Physics.SyncTransforms();

            foreach (var mover in _rigidbodyMovers)
            {
                mover.moverRigidbody.position = mover.beginningTickPosition;
                mover.moverRigidbody.rotation = mover.beginningTickRotation;
                mover.moverRigidbody.MovePosition(mover.currentPosition);
                mover.moverRigidbody.MoveRotation(mover.currentRotation);
            }
        }
    }
}