﻿using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public class ControllableByEventOrbital : MonoBehaviour
    {
        [SerializeField] private TakesOrbitals ownerOfThisOrbital;
        [SerializeField] private OrbitalLogic orbitalLogicToSetTo;

        private GameObject _oldOwner;
        private OrbitalLogic _oldOrbitalLogic;

        public void SetOwnerOfThisOrbital(GameObject owner)
        {
            var orbital = ownerOfThisOrbital.GetOrbital(gameObject);
            if (orbital == null) return;
            _oldOwner = orbital.ownerControlObject;
            _oldOrbitalLogic = orbital.currentOrbitalLogic;
            orbital.ownerControlObject = owner;
            orbital.currentOrbitalLogic = orbitalLogicToSetTo;
            orbital.Initialize(orbital.ownerObject);
        }

        public void UnSetOwnerOfThisOrbital()
        {
            var orbital = ownerOfThisOrbital.GetOrbital(gameObject);
            if (orbital == null) return;
            orbital.ownerControlObject = _oldOwner;
            orbital.currentOrbitalLogic = _oldOrbitalLogic;
            orbital.Initialize(orbital.ownerObject);
        }
    }
}
