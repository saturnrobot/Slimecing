using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    [CreateAssetMenu(fileName = "OrbitalStats", menuName = "Swords/Orbitals/OrbitalStats")]
    public class OrbitalStats : ScriptableObject
    {
        [SerializeField] private float orbitalSpeed;
        [SerializeField] private float yOffset;
        [SerializeField] private float xAxis = 5f;
        [SerializeField] private float yAxis = 5f;

        public float rotationSpeed
        {
            get => orbitalSpeed;
            set => orbitalSpeed = value;
        }

        public float verticalOffset
        {
            get => yOffset;
            set => yOffset = value;
        }

        public float radiusX
        {
            get => xAxis;
            set => xAxis = value;
        }

        public float radiusY
        {
            get => yAxis;
            set => yAxis = value;
        }
    }
}
