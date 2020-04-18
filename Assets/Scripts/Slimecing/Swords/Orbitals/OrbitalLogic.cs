using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public abstract class OrbitalLogic : ScriptableObject
    {
        [SerializeField] private OrbitalStats orbitalStats;
        public OrbitalStats currentOrbitalStats => orbitalStats;

        public abstract void Initialize(GameObject owner, GameObject orbital);
        
        public abstract void Tick(GameObject owner, GameObject orbital);
        
    }
}
