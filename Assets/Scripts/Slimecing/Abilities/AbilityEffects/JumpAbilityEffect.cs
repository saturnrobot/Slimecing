using Slimecing.Characters;
using System;
using System.Collections;
using System.Collections.Generic;
using Slimecing.Abilities.UseAbilities;
using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public class JumpAbilityEffect : AbilityEffect
    {
        private readonly JumpAbility _ability;
        private readonly AbilityUser _aUser;
        private readonly CharacterMovementController _characterMovementController;
        private readonly float _jumpForce;
        private readonly float _doubleForce;
        private readonly float _jumpForceMultiplier;
        private readonly float _simulatedJumpUpMass;
        private readonly float _simulatedFallingMass;
        private readonly int _jumpUseAmount;

        private Vector3 _jumpDir;
        private float _lastJumpMass;
        private bool _fallMassSet;
        private int _currentJumpUseAmount;
        public bool canJump { get; set; }

        public JumpAbilityEffect(Ability ability, AbilityUser aUser, CharacterMovementController controller, 
            float jumpForce, float doubleForce, float jumpForceMultiplier, float simulatedJumpUpMass, 
            float simulatedFallingUpMass, int jumpUseAmount ) : base(ability, aUser)
        {
            _ability = (JumpAbility) ability;
            _aUser = aUser;
            _characterMovementController = controller;
            _jumpForce = jumpForce;
            _doubleForce = doubleForce;
            _jumpForceMultiplier = jumpForceMultiplier;
            _simulatedJumpUpMass = simulatedJumpUpMass;
            _simulatedFallingMass = simulatedFallingUpMass;
            _jumpUseAmount = jumpUseAmount;
        }
        public override void Activate()
        {
            _jumpDir = -_characterMovementController.GravityDirection;
            _lastJumpMass = _characterMovementController.SimulatedFallingMass;
            _characterMovementController.SimulatedFallingMass = _simulatedJumpUpMass;
            BeginJump();
        }
        public override void DoUpdate() {     
            if (canJump)
            {
                _characterMovementController.AddForceTo(_jumpDir, _jumpForceMultiplier);
            }
            else
            {
                if (!_fallMassSet)
                {
                    _characterMovementController.SimulatedFallingMass = _simulatedFallingMass;
                    _fallMassSet = true;
                }

                if (_characterMovementController.IsGrounded())
                {
                    End();
                }
            }
        }

        private void BeginJump()
        {
            _currentJumpUseAmount = _jumpUseAmount;
            _characterMovementController.AddForceTo(_jumpDir, _jumpForce);
            _currentJumpUseAmount--;
        }

        public bool BeginDouble()
        {
            if (_currentJumpUseAmount <= 0) return false;
            _characterMovementController.AddForceTo(_jumpDir, _doubleForce);
            canJump = true;
            _currentJumpUseAmount--;
            return true;
        }

        public override void End()
        {
            _fallMassSet = false;
            _characterMovementController.SimulatedFallingMass = _lastJumpMass;
            _aUser.RemoveAbilityEffect(this);
        }

        public override bool DoesUpdate() => true;
    }
}
