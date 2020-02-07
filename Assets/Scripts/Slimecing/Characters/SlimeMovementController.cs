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
            
            if (_move != Vector2.zero)
            {
                _rotatable.RotateToVector(new Vector3(_move.x, 0, _move.y));
            }
        }
        private void Move()
        {
            rb.MovePosition(rb.position + movementSpeed * Time.fixedDeltaTime * new Vector3(_move.x, 0f, _move.y));
        }
        
        public override void GetMoveInputH(float h)
        {
            _move.x = h;
        }

        public override void GetMoveInputV(float v)
        {
            _move.y = v;
        }
    }
}