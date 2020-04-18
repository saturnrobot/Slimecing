using System;
using Slimecing.Dependency;
using Slimecing.Triggers;
using UnityEngine;

namespace Slimecing.Swords.Orbitals.OrbitalLogicScripts
{
    [CreateAssetMenu(fileName = "InputOrbitalLogic", menuName = "Swords/Orbitals/OrbitalLogic/InputOrbitalLogic")]
    public class InputOrbitalLogic : OrbitalLogic, IOrbitalTickEveryFrame
    {
        [SerializeField] private Trigger orbitalInputTrigger;

        private Trigger _operatingOrbitalInputTrigger;
        private Ellipse _swordEllipse;
        private Vector2 _finalSwordTarget;

        private void OnEnable()
        {
            _swordEllipse = new Ellipse(currentOrbitalStats.radiusX, currentOrbitalStats.radiusY);
            _operatingOrbitalInputTrigger = Instantiate(orbitalInputTrigger);
        }

        private TriggerInput GetInputAbilityActionType()
        {
            if (_operatingOrbitalInputTrigger == null) return null;
            return _operatingOrbitalInputTrigger as TriggerInput;
        }

        public override void Initialize(GameObject owner, GameObject orbital)
        {
            GetInput(owner, orbital, Vector2.up);
            GetInputAbilityActionType()?.ConfigureInput(owner);
        }

        public override void Tick(GameObject owner, GameObject orbital)
        {
            Vector3 center = owner.transform.position;
            
            float step = Time.fixedDeltaTime * currentOrbitalStats.rotationSpeed;
            Vector2 circlePos = _swordEllipse.EvaluateEllipse(_finalSwordTarget.x);
            Vector3 targetPos = new Vector3(circlePos.x, currentOrbitalStats.verticalOffset, circlePos.y);
            Vector3 smoothPos = Vector3.RotateTowards(orbital.transform.position - center, targetPos, 
                step, 0f);
            Vector3 orbitPos = new Vector3(smoothPos.normalized.x * currentOrbitalStats.radiusX, 
                currentOrbitalStats.verticalOffset, smoothPos.normalized.z * currentOrbitalStats.radiusY);
            if (orbitPos != orbital.transform.position)
            {
                orbital.transform.position = orbitPos + center;
            }

            SetLook(owner, orbital);
        }

        private static void SetLook(GameObject owner, GameObject orbital)
        {
            Vector3 position = orbital.transform.position;
            Vector3 ownerPos = owner.transform.position;
            orbital.transform.LookAt(2 * position - new Vector3(ownerPos.x, position.y, ownerPos.z));
        }

        private void GetInput(GameObject owner, GameObject orbital, Vector2 inputDir)
        {
            if (inputDir.Equals(Vector2.zero)) return;
            if (!(Mathf.Abs(inputDir.x) > 0.05f) && !(Mathf.Abs(inputDir.y) > 0.05f)) return;
            Vector3 center = owner.transform.position;
            float angle = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            var position = orbital.transform.position;
            float currentAngle = Mathf.Atan2(position.x - center.x,position.z - center.z) * Mathf.Rad2Deg;
            _finalSwordTarget = new Vector2(angle / 360f, currentAngle / 360f);
        }

        public void TickUpdate(GameObject owner, GameObject orbital)
        {
            if (!_operatingOrbitalInputTrigger.currentTriggerState.Equals(TriggerState.Performed)) return;
            if (!(_operatingOrbitalInputTrigger is TriggerInput orbitalTriggerInput)) return;
            
            if (orbitalTriggerInput.inputContext.control.device.name.Equals("Mouse"))
            {
                if (UnityEngine.Camera.main != null)
                {
                    Plane playerPlane = new Plane(Vector3.up, owner.transform.position);
                    Vector2 readInput = orbitalTriggerInput.inputContext.ReadValue<Vector2>();
                    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(readInput);
                    if (!playerPlane.Raycast(ray, out var distance)) return;
                    Vector3 targetPoint = ray.GetPoint(distance) - owner.transform.position;
                    GetInput(owner, orbital, new Vector2(targetPoint.x, targetPoint.z));
                    return;
                }
            }
            GetInput(owner, orbital, orbitalTriggerInput.inputContext.ReadValue<Vector2>());
        }
    }
}