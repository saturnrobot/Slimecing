using Slimecing.Abilities.UseAbilities;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public class DashAbilityEffect : TimedAbilityEffect
    {
        private DashAbility _ability;
        private readonly AbilityUser _aUser;
        private readonly CharacterMovementController _characterMovementController;
        private readonly float _dashForce;
       
        public DashAbilityEffect(float duration, Ability ability, AbilityUser aUser, 
            CharacterMovementController controller, float dashForce) : base(duration, ability, aUser)
        {
            _aUser = aUser;
            _characterMovementController = controller;
            _dashForce = dashForce;
            _ability = (DashAbility) ability;
        }

        public override void Activate()
        {
            Vector3 dashDir = _aUser.transform.forward;
            if (_characterMovementController.MoveInput != Vector2.zero)
            {
                Vector2 moveInputDir = _characterMovementController.MoveInput;
                dashDir = new Vector3(moveInputDir.x, 0, moveInputDir.y);
            }

            _characterMovementController.AddForceTo(dashDir, _dashForce);
        }
        public override void End()
        {
            _characterMovementController.SetBackToNormalForces();
        }
    }
}
