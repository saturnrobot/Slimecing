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
        private List<OrbitalLogic> _spawnedOrbitalLogic = new List<OrbitalLogic>();

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
                orbitals[i] = new OrbitalPackage(spawnedOrbital, orbitals[i].currentOrbitalLogic);
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

        private void InitializeOrbital(OrbitalPackage orbital)
        {
            AddCollisions(orbital.orbitalObject);
            orbital.currentOrbitalLogic = Instantiate(orbital.currentOrbitalLogic);
            orbital.currentOrbitalLogic.Initialize(gameObject, orbital.orbitalObject);
            ResetSpawnedOrbitalObjects();
        }

        private void ResetSpawnedOrbitalObjects()
        {
            _spawnedOrbitalObjects = new List<GameObject>();
            _spawnedOrbitalLogic = new List<OrbitalLogic>();
            foreach (var t in orbitals)
            {
                _spawnedOrbitalObjects.Add(t.orbitalObject);
                _spawnedOrbitalLogic.Add(t.currentOrbitalLogic);
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
            DropOrbital(orbital.orbitalObject);
            orbitals.Remove(orbital);
        }

        public void Update()
        {
            foreach (var orbital in orbitals)
            {
                if (orbital.currentOrbitalLogic is IOrbitalTickEveryFrame inputOrbitalLogic)
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
                orbital.currentOrbitalLogic.Tick(gameObject, orbital.orbitalObject);
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
                if (orbitals[i] == null)
                {
                    orbitals.Remove(orbitals[i]);
                }
                if (orbitals[i].orbitalObject == _spawnedOrbitalObjects[i] && orbitals[i].currentOrbitalLogic == _spawnedOrbitalLogic[i]) continue;
                //DropOrbital(_spawnedOrbitalObjects[i]);
                InitializeOrbital(orbitals[i]);
            }

            if (resetLoop)
            {
                ResetSpawnedOrbitalObjects();
            }
        }

        private void DropOrbital(GameObject orbital)
        {
            DropLogic dropLogic = orbital.GetComponent<DropLogic>();
            if (dropLogic == null)
            {
                orbital.SetActive(false);
                return;
            }
            
            dropLogic.Drop();
        }
    }
}