using UnityEngine;

namespace Slimecing.Characters
{
    public class SlimeMovementController : CharacterMovementController
    {
        [SerializeField] private float speedOfMovementSmoothing;
        [SerializeField] private float airControl;
        [SerializeField] private float groundControl;
        protected override void Move(float deltaTime)
        {
            if (move == Vector2.zero && rb.velocity == Vector3.zero) return;
            
            bool grounded = IsGrounded();
            
            Vector3 moveInputVector = new Vector3(move.x, 0, move.y).normalized;
            Vector3 up = PlayerUpVector();
            
            Vector3 velocity = rb.velocity;
            
            Vector3 inputRight = Vector3.Cross(moveInputVector, up);
            Vector3 reorientedInput = Vector3.Cross(up, inputRight).normalized * moveInputVector.magnitude;
            Vector3 targetMovementVelocity = reorientedInput * movementSpeed;
            Vector3 smoothedTargetVelocity = Vector3.Lerp(velocity, targetMovementVelocity, 1 - Mathf.Exp(-speedOfMovementSmoothing * deltaTime));
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            Vector3 velocityChange = transform.InverseTransformDirection(smoothedTargetVelocity) - localVelocity;

            velocityChange = Vector3.ClampMagnitude(velocityChange, grounded ? groundControl : airControl);
		    velocityChange = transform.TransformDirection(velocityChange);
            rb.AddForce(velocityChange, ForceMode.VelocityChange);
            internalTransformPosition += velocityChange * deltaTime;

        }

        protected override void Rotate(float deltaTime)
        {
            rotatable.RotateToVector(transform, new Vector3(move.x, 0 , move.y), deltaTime);
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