using System;
using Boo.Lang;
using Slimecing.Characters;
using Slimecing.Environment.Moving;
using UnityEngine;

namespace Slimecing.Environment
{
    public enum SimulationInterpolationMethod
    {
        None,
        Unity
    }
    
    [DefaultExecutionOrder(-100)]
    public class MoverSimulationManager : MonoBehaviour
    {
        private static MoverSimulationManager _instance;
        
        private static readonly List<CharacterMovementController> s_CharacterMovers = new List<CharacterMovementController>();
        private static readonly List<RigidbodyMover> s_RigidbodyMovers = new List<RigidbodyMover>();
        
        private static SimulationInterpolationMethod _internalInterpolationMethod = SimulationInterpolationMethod.None;

        public static SimulationInterpolationMethod interpolationMethod
        {
            get => _internalInterpolationMethod;
            set
            {
                _internalInterpolationMethod = value;

                MoveBodiesInstantly();
                
                RigidbodyInterpolation interpolation = (_internalInterpolationMethod == SimulationInterpolationMethod.Unity) ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;
                foreach (var charMover in s_CharacterMovers)
                {
                    charMover.playerRigidbody.interpolation = interpolation;
                }

                foreach (var mover in s_RigidbodyMovers)
                {
                    mover.moverRigidbody.interpolation = interpolation;
                }
            }
        }
        
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

        public static void RegisterCharMover(CharacterMovementController charMover)
        {
            s_CharacterMovers.Add(charMover);
            
            RigidbodyInterpolation interpolate = _internalInterpolationMethod == SimulationInterpolationMethod.Unity ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;
            charMover.playerRigidbody.interpolation = interpolate;
        }
        
        public static void UnregisterCharMover(CharacterMovementController charMover)
        {
            s_CharacterMovers.Remove(charMover);
        }
        
        public static void RegisterMover(RigidbodyMover mover)
        {
            s_RigidbodyMovers.Add(mover);
            
            RigidbodyInterpolation interpolate = _internalInterpolationMethod == SimulationInterpolationMethod.Unity ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;
            mover.moverRigidbody.interpolation = interpolate;
        }

        public static void UnregisterMover(RigidbodyMover mover)
        {
            s_RigidbodyMovers.Remove(mover);
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
            foreach (var mover in s_RigidbodyMovers)
            {
                var moverTransform = mover.transform;
                mover.beginningTickPosition = moverTransform.position;
                mover.beginningTickRotation = moverTransform.rotation;
            }
        }

        private void SimulateTick(float deltaTime)
        {
            foreach (var mover in s_RigidbodyMovers)
            {
                mover.UpdateMoverVelocity(deltaTime);

                mover.transform.SetPositionAndRotation(mover.currentPosition, mover.currentRotation);
                mover.moverRigidbody.position = mover.currentPosition;
                mover.moverRigidbody.rotation = mover.currentRotation;
            }
            
            foreach (var charMover in s_CharacterMovers)
            {
                charMover.TickCharacter(deltaTime);
            }
        }

        private void PostSimulationTick()
        {
            Physics.SyncTransforms();

            foreach (var mover in s_RigidbodyMovers)
            {
                mover.moverRigidbody.position = mover.beginningTickPosition;
                mover.moverRigidbody.rotation = mover.beginningTickRotation;
                mover.moverRigidbody.MovePosition(mover.currentPosition);
                mover.moverRigidbody.MoveRotation(mover.currentRotation);
            }
        }

        private static void MoveBodiesInstantly()
        {
            foreach (var charMover in s_CharacterMovers)
            {
                charMover.transform.SetPositionAndRotation(charMover.internalTransformPosition,
                    charMover.transform.rotation);
                charMover.playerRigidbody.position = charMover.internalTransformPosition;
                charMover.playerRigidbody.rotation = charMover.transform.rotation;
            }

            foreach (var mover in s_RigidbodyMovers)
            {
                mover.transform.SetPositionAndRotation(mover.currentPosition, mover.currentRotation);
                mover.moverRigidbody.position = mover.currentPosition;
                mover.moverRigidbody.rotation = mover.currentRotation;
            }
        }
    }
}