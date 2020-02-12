using Slimecing.Character;
using Slimecing.Characters;
using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public class DashAbilityEffect : TimedAbilityEffect
    {
        private DashAbility dashAbility;
        private GameObject character;
        private CharacterMovementController characterMovementController;
       
        public DashAbilityEffect(float duration, Ability ability, GameObject obj) : base(duration, ability, obj)
        {
            character = obj;
            characterMovementController = obj.GetComponent<CharacterMovementController>();
            dashAbility = (DashAbility) ability;
        }

        public override void Activate()
        {
            Vector3 dashDir = character.transform.forward;
            if (characterMovementController.MoveInput != Vector2.zero)
            {
                Vector2 moveInputDir = characterMovementController.MoveInput;
                dashDir = new Vector3(moveInputDir.x, 0, moveInputDir.y);
            }

            characterMovementController.AddForceTo(dashDir, dashAbility.DashForce);
        }
        public override void End()
        {
            characterMovementController.SetBackToNormalForces();
        }
    }
}
