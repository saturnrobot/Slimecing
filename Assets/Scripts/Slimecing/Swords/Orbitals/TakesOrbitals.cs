using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public class TakesOrbitals : MonoBehaviour
    {
        [SerializeField] private List<Orbital> orbitals;

        private List<Orbital> _spawnedOrbitals = new List<Orbital>();

        public List<Orbital> spawnedOrbitals => _spawnedOrbitals;

        private void OnEnable()
        {
            SpawnAllOrbitals();
        }

        private void SpawnAllOrbitals()
        {
            foreach (var orbital in orbitals)
            {
                orbital.SpawnOrbital(this);
            }
        }

        private void OnDisable()
        {
            RemoveAllOrbitals();
        }

        private void RemoveAllOrbitals()
        {
            foreach (var orbital in _spawnedOrbitals)
            {
                Destroy(orbital);
            }
        }

        public void AddOrbital(Orbital orbital)
        {
            _spawnedOrbitals.Add( orbital.StartOrbital(orbital,this));
        }

        private void RemoveOrbital(Orbital orbital)
        {
            _spawnedOrbitals.Remove(orbital);
            Destroy(orbital);
        }

        public void Update()
        {
            if (orbitals == spawnedOrbitals) return;
            foreach (var orbital in from orbital in orbitals from spawnedOrbital in spawnedOrbitals where spawnedOrbital != orbital select orbital)
            {
                AddOrbital(orbital);
            }
        }
    }
}
