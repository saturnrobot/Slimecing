﻿using System;
using Slimecing.Dependency;
using Slimecing.Triggers;
using UnityEngine;

namespace Slimecing.Swords.Orbitals.OrbitalLogicScripts
{
    [CreateAssetMenu(fileName = "InputOrbitalLogic", menuName = "Swords/Orbitals/OrbitalLogic/InputOrbitalLogic")]
    public class InputOrbitalLogic : OrbitalLogic
    {
        [SerializeField] private Trigger orbitalInputTrigger;

        private Vector2 _pointPos;
        private Vector3 _center;
        private Vector3 _finalSwordTarget;

        private TriggerInput GetInputAbilityActionType()
        {
            if (orbitalInputTrigger == null) return null;
            return orbitalInputTrigger as TriggerInput;
        }

        public override void Initialize(GameObject owner, GameObject orbital)
        {
            _center = owner.transform.position;
            GetInput(orbital, Vector2.up);
            GetInputAbilityActionType()?.ConfigureInput(owner);
            orbitalInputTrigger.TriggerStateChange += trigger => UpdateOrbitalPos(owner, orbital, trigger);
        }

        private void UpdateOrbitalPos(GameObject owner, GameObject orbital, TriggerPackage trigger)
        {
            if (!trigger.user.Equals(owner)) return;

            GetInput(orbital, trigger.ctx.ReadValue<Vector2>());
        }

        public override void Tick(GameObject owner, GameObject orbital)
        {
            _center = owner.transform.position;

            orbital.transform.position = _finalSwordTarget + _center;

            SetLook(owner, orbital);
        }

        private void SetLook(GameObject owner, GameObject orbital)
        {
            Vector3 position = orbital.transform.position;
            Vector3 ownerPos = owner.transform.position;
            orbital.transform.LookAt(2 * position - new Vector3(ownerPos.x, position.y, ownerPos.z));
        }

        private void GetInput(GameObject orbital, Vector2 inputDir)
        {
            if (!(Mathf.Abs(inputDir.x) > 0.5) && !(Mathf.Abs(inputDir.y) > 0.5)) return;
            float pointX = XAxis * inputDir.x;
            float pointZ = ZAxis * inputDir.y;
            _pointPos = new Vector2(pointX, pointZ);
            Vector3 targetPos = new Vector3(_pointPos.x, YOffset, _pointPos.y);
            /*Vector3 swordTarget = Vector3.RotateTowards(orbital.transform.position - _center,
                targetPos - _center, Time.deltaTime * RotSpeed, 0f);
            Vector3 swordTargetNormalized = swordTarget.normalized;*/
            //_finalSwordTarget = new Vector3(swordTargetNormalized.x * XAxis, YOffset, swordTargetNormalized.z * ZAxis);
            _finalSwordTarget = targetPos;
        }
    }
}
