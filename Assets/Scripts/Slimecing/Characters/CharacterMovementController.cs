using System;
using Slimecing.SimpleComponents.Movement;
using UnityEngine;

namespace Slimecing.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Rotatable))]
    [RequireComponent(typeof(Collider))]
    public abstract class CharacterMovementController : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] protected Rigidbody rb;
        public Rigidbody PlayerRigidbody => rb;
        [SerializeField] protected Rotatable rotatable;
        [SerializeField] private Collider playerCollider;

        [Header("Player Movement Settings")]
        [SerializeField] protected float movementSpeed;
        [SerializeField] private bool useGravity;
        public bool UseGravity { get => useGravity;
            set => useGravity = value;
        }
        [SerializeField] protected Vector3 gravityDirection = Vector3.down;
        public Vector3 GravityDirection { get => gravityDirection;
            set => gravityDirection = value;
        }
        [SerializeField] protected float gravityForce;
        public float GravityForce { get => gravityForce;
            set => gravityForce = value;
        }
        [SerializeField] protected float simulatedFallingMass;
        public float SimulatedFallingMass { get => simulatedFallingMass;
            set => simulatedFallingMass = value;
        }
        [SerializeField] protected float minVelocityMagnitude;

        [Header("Slope Settings")]
        [SerializeField] private Vector3 slopeCheckPoint;
        [SerializeField] protected float slopeCheckRadius;
        [SerializeField] protected float maxAngle;

        [Header("Ledge Settings")]
        [SerializeField] private Vector3 ledgeCheckPoint;
        [SerializeField] private float ledgeCheckRadius;

        [Header("Grounding Settings")]
        [SerializeField] private Vector3 castOrigin;
        [SerializeField] protected float groundProbingDistance;
        [SerializeField] protected float groundingSphereCastRadius;
        [SerializeField] private LayerMask hitLayerMask;

        [Header("Other Settings")]
        [SerializeField] const int MaxHitsBuffer = 3;

        protected Vector2 move;
        protected Vector3 calculatedGravity;

        private bool _isGrounded;
        public bool IsGrounded() => _isGrounded;
        private bool _canBeGrounded;

        

        private RaycastHit _internalGroundHit;
        private RaycastHit _internalSlopeHit;
        private RaycastHit _lastGoodNormalHit;
        private RaycastHit[] _internalCharacterHits = new RaycastHit[MaxHitsBuffer];

        public Vector2 MoveInput => move;

        protected void Awake()
        {
            calculatedGravity = gravityDirection * gravityForce;
        }

        public abstract void GetMoveInputH(float h);
        public abstract void GetMoveInputV(float v);

        public void AddForceTo(Vector3 direction, float amount)
        {
            rb.AddForce(direction.normalized * amount, ForceMode.Impulse);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.TransformPoint(slopeCheckPoint), slopeCheckRadius);

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.TransformPoint(ledgeCheckPoint), ledgeCheckRadius);

            Color changeColor = _isGrounded ? Color.green : Color.red;
            Gizmos.color = changeColor;
            Gizmos.DrawRay(transform.TransformPoint(castOrigin), gravityDirection.normalized * groundingSphereCastRadius);
            Gizmos.DrawWireSphere(transform.TransformPoint(castOrigin), groundingSphereCastRadius);

            Debug.DrawLine(transform.position, transform.position + PlayerUpVector() * 2, Color.cyan);
            Debug.DrawLine(transform.position, transform.position + Forward() * 2, Color.yellow);
        }

        protected virtual void FixedUpdate()
        {
            GroundCheck(groundProbingDistance, groundingSphereCastRadius);
            
            float deltaTime = Time.deltaTime;
            
            Rotate(deltaTime);
            Move(deltaTime);

            if (!_isGrounded && useGravity) rb.AddForce(simulatedFallingMass * calculatedGravity);

            if (rb.velocity.magnitude < minVelocityMagnitude)
            {
                rb.velocity = Vector3.zero;
            }

            _isGrounded = false;
        }

        protected abstract void Move(float deltaTime);

        protected abstract void Rotate(float deltaTime);

        public float GetGroundAngle()
        {
            if (!_isGrounded) return 90f;
            return Vector3.Angle(_internalGroundHit.normal, transform.forward);
        }

        private bool IsStableOnNormal(Vector3 normal)
        {
            return Vector3.Angle(transform.up, normal) <= maxAngle;
        }

        protected Vector3 PlayerUpVector()
        {
            if (!_isGrounded) return transform.up;
            if (_internalSlopeHit.transform == null) return transform.up;
            return _internalSlopeHit.normal;
        }
        protected Vector3 Forward()
        {
            if (!_isGrounded) return transform.forward;
            if (_internalSlopeHit.transform == null) return transform.forward;
            return Vector3.Cross(transform.right, _internalSlopeHit.normal);
        }
        public void SetBackToNormalForces()
        {
            rb.velocity = new Vector3(move.x * movementSpeed, rb.velocity.y, move.y * movementSpeed);
        }
        
        public void GroundCheck(float length, float radius)
        {
            var ray =
                new Ray(
                    transform.TransformPoint(castOrigin),
                    gravityDirection);

            if (GroundSweep(ray, radius, length, out var groundSweepHit))
            {
                _canBeGrounded = true;
                SlopeColliderValidate(groundSweepHit, slopeCheckRadius);
            }
            else
            {
                _canBeGrounded = false;
                _isGrounded = false;
            }
        }

        private bool GroundSweep(Ray position, float radius, float distance, out RaycastHit closestHit)
        {
            closestHit = new RaycastHit();
            int num =
                Physics.SphereCastNonAlloc(
                    position,
                    radius,
                    _internalCharacterHits,
                    distance,
                    hitLayerMask,
                    QueryTriggerInteraction.Ignore
                    );

            bool foundValidHit = false;
            float closestDistance = Mathf.Infinity;

            for (int i = 0; i < num; i++)
            {
                if (_internalCharacterHits[i].distance > 0f)
                {
                    if (_internalCharacterHits[i].distance < closestDistance)
                    {
                        closestHit = _internalCharacterHits[i];
                        closestHit.distance -= 0.1f;
                        closestDistance = _internalCharacterHits[i].distance;
                        _internalGroundHit = closestHit;
                        foundValidHit = true;
                    }
                }
            }

            return foundValidHit;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (_canBeGrounded)
            {
                float minFlatness = 1f - Mathf.Sin(Mathf.Deg2Rad * maxAngle);
                for (int i = 0; i < collision.contactCount; i++)
                {
                    if (i > MaxHitsBuffer) break;

                    ContactPoint c = collision.GetContact(i);

                    float flatness = Vector3.Dot(Vector3.up, c.normal);
                    if (flatness < 0f)
                        continue;

                    if (flatness >= minFlatness)
                    {
                        _isGrounded = true;
                    }
                }
            }
        }

        private void SlopeColliderValidate(RaycastHit tempHit, float radius)
        {
            Collider[] colBuffer = new Collider[MaxHitsBuffer];
            int num =
            Physics.OverlapSphereNonAlloc(
                transform.TransformPoint(slopeCheckPoint),
                radius,
                colBuffer,
                hitLayerMask,
                QueryTriggerInteraction.Ignore
                );

            if (LedgeCheck(ledgeCheckRadius)) {
                _internalSlopeHit = _lastGoodNormalHit;
                return;
            }

            for (int i = 0; i < num; i++)
            {
                RaycastHit hit;
                Physics.Linecast(transform.position, colBuffer[i].transform.position, out hit, hitLayerMask, QueryTriggerInteraction.Ignore);
                if (((colBuffer[i].transform != tempHit.transform) || (hit.normal != tempHit.normal)) && IsStableOnNormal(tempHit.normal) && CheckIfValidCollider(hit.collider))
                {
                    if (IsStableOnNormal(hit.normal)) 
                    {
                        _internalSlopeHit = hit;
                        _lastGoodNormalHit = _internalSlopeHit;
                    }
                    
                    return;
                }
            }

            if(IsStableOnNormal(tempHit.normal))
            {
                _internalSlopeHit = _internalGroundHit;
                _lastGoodNormalHit = _internalSlopeHit;
                return;
            }

            _internalSlopeHit = _lastGoodNormalHit;
        }

        private bool LedgeCheck(float radius)
        {
            Collider[] colBuffer = new Collider[MaxHitsBuffer];
            int num =
            Physics.OverlapSphereNonAlloc(
                transform.TransformPoint(ledgeCheckPoint),
                radius,
                colBuffer,
                hitLayerMask,
                QueryTriggerInteraction.Ignore
                );

            if (num.Equals(0))
            {
                return true;
            }

            for (int i = 0; i < num; i++)
            {
                if (CheckIfValidCollider(colBuffer[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckIfValidCollider(Collider col)
        {
            if (col != playerCollider && col != null)
            {
                return true;
            }

            return false;
        }

    }
}
