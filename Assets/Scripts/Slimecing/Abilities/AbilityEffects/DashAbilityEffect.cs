<<<<<<< HEAD
﻿using Slimecing.Character;
using Slimecing.Characters;
=======
﻿using Slimecing.Characters;
>>>>>>> Added triggers and lots of backend
using UnityEngine;

namespace Slimecing.Abilities.AbilityEffects
{
    public class DashAbilityEffect : TimedAbilityEffect
    {
<<<<<<< HEAD
        private DashAbility dashAbility;
        private GameObject character;
        private CharacterMovementController characterMovementController;
       
        public DashAbilityEffect(float duration, Ability ability, GameObject obj) : base(duration, ability, obj)
        {
            character = obj;
            characterMovementController = obj.GetComponent<CharacterMovementController>();
            dashAbility = (DashAbility) ability;
=======
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
>>>>>>> Added triggers and lots of backend
        }

        public override void Activate()
        {
<<<<<<< HEAD
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
=======
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
>>>>>>> Added triggers and lots of backend
        }
    }
}
