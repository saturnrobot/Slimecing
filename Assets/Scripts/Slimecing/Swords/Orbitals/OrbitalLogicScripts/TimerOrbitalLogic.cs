using UnityEngine;
using Random = UnityEngine.Random;

namespace Slimecing.Swords.Orbitals.OrbitalLogicScripts
{
    [CreateAssetMenu(fileName = "TimerOrbitalLogic", menuName = "Swords/Orbitals/OrbitalLogic/TimerOrbitalLogic")]
    public class TimerOrbitalLogic : OrbitalLogic, IOrbitalTickEveryFrame
    {
        [SerializeField] private bool randomProgress;
        [SerializeField] private float startOrbitalProgress;
        
        public override void Initialize(Orbital orbital)
        {
            orbital.orbitalProgress = randomProgress ? Random.Range(0.0f, 1.0f) : startOrbitalProgress;
            SetOrbitalPos(orbital);
        }
        public override void Tick(Orbital orbital) { }

        private void SetOrbitalPos(Orbital orbital)
        {
            Vector2 orbitPos = orbital.orbitPath.EvaluateEllipse(orbital.orbitalProgress, orbital.radiusX, orbital.radiusY);
            Vector3 pos = new Vector3(orbitPos.x, orbital.verticalOffset, orbitPos.y);
            orbital.orbitalObject.transform.position = pos + orbital.ownerObject.transform.position;
        }

        private static void SetLook(GameObject owner, GameObject orbital)
        {
            Vector3 position = orbital.transform.position;
            Vector3 ownerPos = owner.transform.position;
            orbital.transform.LookAt(2 * position - new Vector3(ownerPos.x, position.y, ownerPos.z));
        }

        private void Rotate(Orbital orbital)
        {
            float orbitSpeed = 1f / orbital.rotationSpeed;
            orbital.orbitalProgress += Time.deltaTime * orbitSpeed;
            orbital.orbitalProgress %= 1f;
            SetOrbitalPos(orbital);
        }

        public void TickUpdate(Orbital orbital)
        {
            if (Mathf.Abs(orbital.rotationSpeed) < 0.1)
            {
                orbital.rotationSpeed = 0.1f;
            }
            Rotate(orbital);
            SetLook(orbital.ownerObject, orbital.orbitalObject);
        }
    }
}
