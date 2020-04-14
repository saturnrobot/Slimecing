using Slimecing.Abilities.UseAbilities;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public class SimpleJumpAbilityEffect : AbilityEffect
    {
        private readonly JumpAbility _ability;
        private readonly AbilityUser _aUser;
        private readonly CharacterMovementController _controller;
        private readonly float _jumpForce;
        private readonly float _simulatedJumpUpMass;
        private readonly float _simulatedFallingMass;

        private Vector3 _jumpDir;
        private float _lastJumpMass;
        private bool _fallMassSet;
       
        public bool canJump { get; set; }

        public SimpleJumpAbilityEffect(Ability ability, AbilityUser aUser, CharacterMovementController controller, 
            float jumpForce, float simulatedJumpUpMass, float simulatedFallingUpMass) : base(ability, aUser)
        {
            _ability = (JumpAbility)ability;
            _aUser = aUser;
            _controller = controller;
            _jumpForce = jumpForce;
            _simulatedJumpUpMass = simulatedJumpUpMass;
            _simulatedFallingMass = simulatedFallingUpMass;
        }

        public override void Activate()
        {
            _jumpDir = -_controller.GravityDirection;
            _lastJumpMass = _controller.SimulatedFallingMass;
            _controller.SimulatedFallingMass = _simulatedJumpUpMass;
            _controller.AddForceTo(-_jumpDir, _controller.PlayerRigidbody.velocity.y);
            BeginJump();
        }

        public override void DoUpdate()
        {
            if (canJump)
            {
                Debug.Log("ayy");
                return;
            }
            if (!_fallMassSet)
            {
                _controller.SimulatedFallingMass = _simulatedFallingMass;
                _fallMassSet = true;
            }

            if (_controller.IsGrounded())
            {
                End();
            }
        }

        private void BeginJump()
        {
            _controller.AddForceTo(_jumpDir, _jumpForce);
        }

        public override bool DoesUpdate() => true;

        public override void End()
        {
            _fallMassSet = false;
            _controller.SimulatedFallingMass = _lastJumpMass;
            _aUser.RemoveAbilityEffect(this);
        }
    }
}
