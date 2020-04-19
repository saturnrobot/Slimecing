using System;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    [Serializable]
    public class OrbitalPackage
    {

        [SerializeField] private GameObject orbital;
        [SerializeField] private OrbitalLogic orbitalLogic;
        [SerializeField] private bool sticky;
        
        public bool orbitalSticky
        {
            get => sticky;
            set => sticky = value;
        }

        public OrbitalPackage(GameObject orbital, OrbitalLogic orbitalLogic)
        {
            this.orbital = orbital;
            this.orbitalLogic = orbitalLogic;
            sticky = false;
        }
        
        public OrbitalPackage(GameObject orbital, OrbitalLogic orbitalLogic, bool sticky)
        {
            this.orbital = orbital;
            this.orbitalLogic = orbitalLogic;
            this.sticky = sticky;
        }

        public GameObject orbitalObject
        {
            get => orbital;
            set => orbital = value;
        }

        public OrbitalLogic currentOrbitalLogic
        {
            get => orbitalLogic;
            set => orbitalLogic = value;
        }
    }
}
