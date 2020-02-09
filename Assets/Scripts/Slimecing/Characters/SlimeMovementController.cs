using Slimecing.Character;
using Slimecing.SimpleComponents.Movement;
using UnityEngine;

namespace Slimecing.Characters
{
    [RequireComponent(typeof(RbRotatable))]
    public class SlimeMovementController : CharacterMovementController
    {
        private RbRotatable _rotatable;

        protected override void Awake()
        {
            base.Awake();
            _rotatable = GetComponent<RbRotatable>();
        }
        private void FixedUpdate()
        {
            Move();
            
            if (move != Vector2.zero)
            {
                _rotatable.RotateToVector(new Vector3(move.x, 0, move.y));
            }
        }
        private void Move()
        {
            rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * new Vector3(move.x, 0f, move.y));
        }
        
        public override void GetMoveInputH(float h)
        {
            move.x = h;
        }

        public override void GetMoveInputV(float v)
        {
            move.y = v;
        }
    }
}