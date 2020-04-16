using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Slimecing.Swords.Orbitals.OrbitalLogicScripts;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public class TakesOrbitals : MonoBehaviour
    {
        [SerializeField] private OrbitalLogic defaultOrbitalLogic;
        [SerializeField] private List<OrbitalPackage> orbitals;

        private Collider _collider;
        
        private List<GameObject> _spawnedOrbitalObjects = new List<GameObject>();
        private int _currentListCount = 0;

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            SpawnAllOrbitals();
        }

        private void SpawnAllOrbitals()
        {
            for (int i = 0; i < orbitals.Count; i++)
            {
                var thisTransform = transform;
                GameObject spawnedOrbital = Instantiate(orbitals[i].orbitalObject, thisTransform.position,
                    Quaternion.identity, thisTransform);
                orbitals[i] = new OrbitalPackage(spawnedOrbital, orbitals[i].OrbitalLogic);
                InitializeOrbital(orbitals[i]);
            }
        }

        private void OnDisable()
        {
            RemoveAllOrbitals();
        }

        private void RemoveAllOrbitals()
        {
            foreach (var orbital in orbitals)
            {
                Destroy(orbital.orbitalObject);
            }
        }

        public void InitializeOrbital(OrbitalPackage orbital)
        {
            Debug.Log("started");
            AddCollisions(orbital.orbitalObject);
            orbital.OrbitalLogic = Instantiate(orbital.OrbitalLogic);
            orbital.OrbitalLogic.Initialize(gameObject, orbital.orbitalObject);
            _currentListCount = orbitals.Count;
            ResetSpawnedOrbitalObjects();
        }

        private void ResetSpawnedOrbitalObjects()
        {
            _spawnedOrbitalObjects = new List<GameObject>();
            foreach (var t in orbitals)
            {
                _spawnedOrbitalObjects.Add(t.orbitalObject);
            }
        }

        public void AddOrbital(GameObject orbital, OrbitalLogic orbitalLogic, bool stick)
        {
            ActuallyAddOrbital(new OrbitalPackage(orbital, orbitalLogic, stick));
        }

        public void AddOrbital(GameObject orbital, bool stick)
        {
            ActuallyAddOrbital(new OrbitalPackage(orbital, defaultOrbitalLogic, stick));
        }

        private void ActuallyAddOrbital(OrbitalPackage orbitalPackage)
        {
            InitializeOrbital(orbitalPackage);
            orbitals.Add(orbitalPackage);
        }

        private void AddCollisions(GameObject orbital)
        {
            Collider orbitalCollider = orbital.GetComponent<Collider>();
            if (_collider != null && orbitalCollider != null)
            {
                Physics.IgnoreCollision(_collider, orbitalCollider);
                foreach (var spawnedOrbital in orbitals)
                {
                    Collider spawnedOrbitalCollider = spawnedOrbital.orbitalObject.GetComponent<Collider>();
                    if (spawnedOrbitalCollider == null) continue;
                    Physics.IgnoreCollision(orbitalCollider, spawnedOrbitalCollider);
                }
            }
        }

        private void RemoveOrbital(OrbitalPackage orbital)
        {
            orbitals.Remove(orbital);
        }

        public void Update()
        {
            ValidateOrbitals();
            
            foreach (var orbital in orbitals)
            {
                orbital.OrbitalLogic.Tick(gameObject, orbital.orbitalObject);
            }
        }

        private void ValidateOrbitals()
        {
            for (int i = 0; i < _spawnedOrbitalObjects.Count; i++)
            {
                if (orbitals[i].orbitalObject != _spawnedOrbitalObjects[i])
                {
                    InitializeOrbital(orbitals[i]);
                }
            }
            
            if (orbitals.Count > _currentListCount)
            {
                ResetSpawnedOrbitalObjects();
            }
        }
    }
}