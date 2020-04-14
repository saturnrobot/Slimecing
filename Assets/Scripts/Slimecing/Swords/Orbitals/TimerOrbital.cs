using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public class TimerOrbital : Orbital
    {
        private float _timer;
        protected override void UpdateSword()
        {
            _timer += Time.deltaTime;
            Rotate();
            SetLook();
        }

        protected override void SetLook()
        {
            Vector3 position = transform.position;
            Vector3 ownerPos = owner.transform.position;
            transform.LookAt(2 * position - new Vector3(ownerPos.x, position.y, ownerPos.z));
        }

        protected override void Rotate()
        {
            if (IsownerNull || IsstatsNull) return;
            float x = -Mathf.Cos(_timer * Stats.RotSpeed) * Stats.XAxis;
            float z = Mathf.Sin(_timer *  Stats.RotSpeed) * Stats.ZAxis;

            Vector3 ownerPosition = owner.transform.position;
            Vector3 pos = new Vector3(x, Stats.YOffset, z);
            transform.position = pos + ownerPosition;
        }
    }
}
