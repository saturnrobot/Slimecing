using Slimecing.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slimecing.Swords.Orbitals.OrbitalLogicScripts
{
    [CreateAssetMenu(fileName = "InputOrbitalLogic", menuName = "Swords/Orbitals/OrbitalLogic/InputOrbitalLogic")]
    public class InputOrbitalLogic : OrbitalLogic, IOrbitalTickEveryFrame, IControllableOrbital
    {
        [SerializeField] private Trigger orbitalInputTrigger;
        
        private Trigger runtimeOrbitalInputTrigger { get; set; }
        
        public override OrbitalLogic GetOrbital()
        {
            return Instantiate(this);
        }
        
        public override void Initialize(Orbital orbital)
        {
            runtimeOrbitalInputTrigger = orbitalInputTrigger.GetTrigger();
            GetInput(orbital, Vector2.up);
            runtimeOrbitalInputTrigger.EnableTrigger(orbital.ownerControlObject);
        }

        public override void Tick(Orbital orbital)
        {
            Vector3 center = orbital.ownerObject.transform.position;
            
            float step = Time.fixedDeltaTime * orbital.rotationSpeed;
            Vector2 circlePos = orbital.orbitPath.EvaluateEllipse(orbital.orbitalProgress, orbital.radiusX, orbital.radiusY);
            Vector3 targetPos = new Vector3(circlePos.x, orbital.verticalOffset, circlePos.y);
            Vector3 smoothPos = Vector3.RotateTowards(orbital.orbitalObject.transform.position - center, 
                targetPos,step, 0f);
            Vector3 orbitPos = new Vector3(smoothPos.normalized.x * orbital.radiusX, 
                orbital.verticalOffset, smoothPos.normalized.z * orbital.radiusY);
            if (orbitPos != orbital.orbitalObject.transform.position)
            {
                orbital.orbitalObject.transform.position = orbitPos + center;
            }

            SetLook(orbital.ownerObject, orbital.orbitalObject);
        }

        private static void SetLook(GameObject owner, GameObject orbital)
        {
            Vector3 position = orbital.transform.position;
            Vector3 ownerPos = owner.transform.position;
            orbital.transform.LookAt(2 * position - new Vector3(ownerPos.x, position.y, ownerPos.z));
        }

        private void GetInput(Orbital orbital, Vector2 inputDir)
        {
            if (inputDir.Equals(Vector2.zero)) return;
            if (!(Mathf.Abs(inputDir.x) > 0.05f) && !(Mathf.Abs(inputDir.y) > 0.05f)) return;
            Vector3 center = orbital.ownerObject.transform.position;
            float angle = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
            orbital.orbitalProgress = angle / 360f;
        }

        public void TickUpdate(Orbital orbital)
        {
            var action = runtimeOrbitalInputTrigger.ReadCurrentValue<InputAction>();
            if (action == null) return;
            if (!action.triggered) return;
            if (action.activeControl != null && action.activeControl?.device.name == "Mouse")
            {
                if (UnityEngine.Camera.main != null)
                {
                    Plane playerPlane = new Plane(Vector3.up, orbital.ownerObject.transform.position);
                    Vector2 readInput = action.ReadValue<Vector2>();
                    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(readInput);
                    if (!playerPlane.Raycast(ray, out var distance)) return;
                    Vector3 targetPoint = ray.GetPoint(distance) - orbital.ownerObject.transform.position;
                    GetInput(orbital, new Vector2(targetPoint.x, targetPoint.z));
                    return;
                }
            }
            GetInput(orbital, action.ReadValue<Vector2>());
        }

        public void ChangeController(GameObject controller)
        {
            if (runtimeOrbitalInputTrigger == null) return;
            runtimeOrbitalInputTrigger.EnableTrigger(controller);
        }
    }
}