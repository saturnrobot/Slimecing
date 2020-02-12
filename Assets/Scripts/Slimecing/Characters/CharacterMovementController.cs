using Slimecing.SimpleComponents.Movement;
using UnityEngine;

namespace Slimecing.Characters
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Rotatable))]
    [RequireComponent(typeof(Collider))]
    public abstract class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected Vector3 gravityDirection = Vector3.down;
        [SerializeField] protected float gravityForce;
        [SerializeField] protected float simulatedMass;
        [SerializeField] protected float groundDistance;
        [SerializeField] protected float slopeDistance;
        [SerializeField] protected float maxAngle;
        [SerializeField] protected float minVelocityMagnitude;

        protected Rigidbody rb;
        protected Rotatable rotatable;
        protected Vector2 move;
        protected Vector3 calculatedGravity;
        protected bool isGrounded;

        private Collider _collider;
        [SerializeField] private LayerMask groundLayerMask;

        public Vector2 MoveInput => move;
        protected void Awake()
        {
            _collider = GetComponent<Collider>();
            rb = GetComponent<Rigidbody>();
            rotatable = GetComponent<Rotatable>();
            calculatedGravity = gravityDirection * gravityForce;
        }

        public abstract void GetMoveInputH(float h);
        public abstract void GetMoveInputV(float v);

        public void AddForceTo(Vector3 direction, float amount)
        {
            rb.AddForce(direction.normalized * amount, ForceMode.Impulse);
        }

        private void Update()
        {
            isGrounded = Grounded(gravityDirection, groundDistance);
            var position = transform.position;
            Debug.DrawLine(position, position + slopeDistance * 2 * Forward(), Color.magenta);
        }

        protected virtual void FixedUpdate()
        {
            float deltaTime = Time.deltaTime;
            
            Rotate(deltaTime);
            Move(deltaTime);
            
            if (!isGrounded) rb.AddForce(simulatedMass * calculatedGravity);

            if (rb.velocity.magnitude < minVelocityMagnitude)
            {
                rb.velocity = Vector3.zero;
            }
        }

        protected abstract void Move(float deltaTime);

        protected abstract void Rotate(float deltaTime);

        public float GetGroundAngle()
        {
            if (!isGrounded) return 90f;
            return Vector3.Angle(SlopeCheck(gravityDirection, slopeDistance).normal, transform.forward);
        }

        protected Vector3 Forward()
        {
            if (!isGrounded) return transform.forward;
            return Vector3.Cross(transform.right, SlopeCheck(gravityDirection, slopeDistance).normal);
        }
        public void SetBackToNormalForces()
        {
            rb.velocity = new Vector3(move.x * movementSpeed, rb.velocity.y, move.y * movementSpeed);
        }
        public bool Grounded(Vector3 direction, float length)
        {
            return Physics.CheckSphere(transform.position, length, groundLayerMask, QueryTriggerInteraction.Ignore);
        }

        public RaycastHit SlopeCheck(Vector3 direction, float length)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, direction, out hit, length, groundLayerMask,
                QueryTriggerInteraction.Ignore);
            return hit;
        }
        private bool ValidateColliderForCollisions(Collider coll)
        {
            return coll != null && coll != _collider;
        }

    }
}
