using UnityEngine;

namespace Slimecing.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class CharacterMovementController : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed;

        protected Rigidbody rb;
        protected Vector2 move;
        
        public Vector2 MoveInput => move;
        
        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public abstract void GetMoveInputH(float h);
        public abstract void GetMoveInputV(float v);

        public void AddForceTo(Vector3 direction, float amount)
        {
            rb.AddForce(direction.normalized * amount, ForceMode.Impulse);
        }

        public void ResetForces()
        {
            rb.velocity = Vector3.zero;
        }

        public void SetBackToNormalForces()
        {
            rb.velocity = new Vector3(move.x, rb.velocity.y, move.y);
        }
    }
}
