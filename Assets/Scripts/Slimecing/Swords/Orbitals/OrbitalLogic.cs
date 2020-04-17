using Slimecing.Triggers;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public abstract class OrbitalLogic : ScriptableObject
    {
        [SerializeField] private float rotSpeed;
        [SerializeField] private float yOffset;
        [SerializeField] private float xAxis = 5f;
        [SerializeField] private float zAxis = 5f;

        public float RotSpeed
        {
            get => rotSpeed;
            set => rotSpeed = value;
        }

        public float YOffset
        {
            get => yOffset;
            set => yOffset = value;
        }

        public float XAxis
        {
            get => xAxis;
            set => xAxis = value;
        }

        public float ZAxis
        {
            get => zAxis;
            set => zAxis = value;
        }

        public abstract void Initialize(GameObject owner, GameObject orbital);
        
        public abstract void Tick(GameObject owner, GameObject orbital);
    }
}
