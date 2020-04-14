using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    [CreateAssetMenu(fileName = "OrbitalStats", menuName = "Swords/BasicStats/OrbitalStats")]
    public class OrbitalStats : ScriptableObject
    {
        [SerializeField] private float rotSpeed;
        [SerializeField] private float yOffset;
        [SerializeField] private float xAxis = 5f;
        [SerializeField] private float zAxis = 5f;

        public float RotSpeed => rotSpeed;

        public float YOffset => yOffset;

        public float XAxis => xAxis;

        public float ZAxis => zAxis;
        
    }
}
