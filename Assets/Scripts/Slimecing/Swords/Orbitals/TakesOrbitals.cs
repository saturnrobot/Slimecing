using System;
using System.Collections.Generic;
using Slimecing.Swords.DropBehaviour;
using Slimecing.Swords.Orbitals.OrbitalLogicScripts;
using Slimecing.Triggers;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public class TakesOrbitals : MonoBehaviour
    {
        [SerializeField] private OrbitalLogic defaultOrbitalLogic;
        [SerializeField] private List<OrbitalPackage> orbitals;

        private Collider _collider;
        
        private List<GameObject> _spawnedOrbitalObjects = new List<GameObject>();

        private void OnEnable()
        {
            _collider = GetComponent<Collider>();
            SpawnAllOrbitals();
        }

        private void SpawnAllOrbitals()
        {
            var thisTransform = transform;
            for (int i = 0; i < orbitals.Count; i++)
            {
                GameObject spawnedOrbital = Instantiate(orbitals[i].orbitalObject, thisTransform.position,
                    Quaternion.identity);
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
            AddCollisions(orbital.orbitalObject);
            orbital.OrbitalLogic = Instantiate(orbital.OrbitalLogic);
            orbital.OrbitalLogic.Initialize(gameObject, orbital.orbitalObject);
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
            DropOrbital(orbital);
            orbitals.Remove(orbital);
        }

        public void Update()
        {
            foreach (var orbital in orbitals)
            {
                if (orbital.OrbitalLogic is IOrbitalTickEveryFrame inputOrbitalLogic)
                {
                    inputOrbitalLogic.TickUpdate(gameObject, orbital.orbitalObject);
                }
            }
        }

        public void FixedUpdate()
        {
            ValidateOrbitals();
            
            foreach (var orbital in orbitals)
            {
                orbital.OrbitalLogic.Tick(gameObject, orbital.orbitalObject);
            }
        }

        private void ValidateOrbitals()
        {
            float loopCount = _spawnedOrbitalObjects.Count;
            bool resetLoop = false;
            
            if (orbitals.Count != _spawnedOrbitalObjects.Count)
            {
                resetLoop = true;
                if (orbitals.Count < _spawnedOrbitalObjects.Count)
                {
                    loopCount = orbitals.Count;
                }
            }
            
            for (int i = 0; i < loopCount; i++)
            {
                if (orbitals[i].orbitalObject != _spawnedOrbitalObjects[i])
                {
                    DropOrbital(orbitals[i]);
                    InitializeOrbital(orbitals[i]);
                }
            }

            if (resetLoop)
            {
                ResetSpawnedOrbitalObjects();
            }
        }

        private void DropOrbital(OrbitalPackage orbital)
        {
            DropLogic dropLogic = orbital.orbitalObject.GetComponent<DropLogic>();
            if (dropLogic == null)
            {
                if (orbital.orbitalSticky)
                {
                    orbital.orbitalObject.SetActive(false);
                    return;
                }
                Destroy(orbital.orbitalObject);
                return;
            }
            
            dropLogic.Drop();
        }
    }
}