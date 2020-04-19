using System;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public abstract class OrbitalLogic : ScriptableObject
    {
        public virtual OrbitalLogic GetOrbital()
        {
            return this;
        }
        public abstract void Initialize(Orbital orbital);
        public abstract void Tick(Orbital orbital);
        
    }
}
