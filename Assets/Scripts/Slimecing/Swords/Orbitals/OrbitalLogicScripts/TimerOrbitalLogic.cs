using System;
using Slimecing.Dependency;
using Slimecing.Triggers;
using Slimecing.Triggers.TriggerLogic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Slimecing.Swords.Orbitals.OrbitalLogicScripts
{
    [CreateAssetMenu(fileName = "TimerOrbitalLogic", menuName = "Swords/Orbitals/OrbitalLogic/TimerOrbitalLogic")]
    public class TimerOrbitalLogic : OrbitalLogic, IOrbitalTickEveryFrame
    {
        [SerializeField] private bool randomProgress;
        [SerializeField] private float orbitalProgress;
        
        private Ellipse _orbitPath;

        private void OnEnable()
        {
            _orbitPath = new Ellipse(currentOrbitalStats.radiusX, currentOrbitalStats.radiusY);
        }

        public override void Initialize(GameObject owner, GameObject orbital)
        {
            if (randomProgress) orbitalProgress = Random.Range(0.0f, 1.0f);
            SetOrbitalPos(owner, orbital);
        }
        public override void Tick(GameObject owner, GameObject orbital) { }

        private void SetOrbitalPos(GameObject owner, GameObject orbital)
        {
            Vector2 orbitPos = _orbitPath.EvaluateEllipse(orbitalProgress);
            Vector3 pos = new Vector3(orbitPos.x, currentOrbitalStats.verticalOffset, orbitPos.y);
            orbital.transform.position = pos + owner.transform.position;
        }

        private static void SetLook(GameObject owner, GameObject orbital)
        {
            Vector3 position = orbital.transform.position;
            Vector3 ownerPos = owner.transform.position;
            orbital.transform.LookAt(2 * position - new Vector3(ownerPos.x, position.y, ownerPos.z));
        }

        private void Rotate(GameObject owner, GameObject orbital)
        {
            float orbitSpeed = 1f / currentOrbitalStats.rotationSpeed;
            orbitalProgress += Time.deltaTime * orbitSpeed;
            orbitalProgress %= 1f;
            SetOrbitalPos(owner, orbital);
        }

        public void TickUpdate(GameObject owner, GameObject orbital)
        {
            if (Mathf.Abs(currentOrbitalStats.rotationSpeed) < 0.1)
            {
                currentOrbitalStats.rotationSpeed = 0.1f;
            }
            Rotate(owner, orbital);
            SetLook(owner, orbital);
        }
    }
}
