using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public class TakesOrbitals : MonoBehaviour
    {
        [SerializeField] private List<Orbital> orbitals;

        private void OnEnable()
        {
            SpawnAllOrbitals();
        }

        private void SpawnAllOrbitals()
        {
            var thisTransform = transform;
            foreach (var orbital in orbitals)
            {
                if (orbital.instantiateOrbital)
                {
                    orbital.orbitalObject = Instantiate(orbital.orbitalObject, thisTransform.position,
                        Quaternion.identity);
                }

                InitializeCurrentOrbital(orbital);
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

        private void InitializeCurrentOrbital(Orbital orbital)
        {
            orbital.Initialize(gameObject);
            SetCollisions(orbital);
        }

        private void ActuallyAddOrbital(Orbital orbital)
        {
            InitializeCurrentOrbital(orbital);
            orbitals.Add(orbital);
        }

        private void SetCollisions(Orbital orbital)
        {
            foreach (var orbitalCollider in orbitals.Select(o => 
                o.orbitalObject.GetComponent<Collider>()).Where(orbitalCollider => orbitalCollider != null))
            {
                orbital.SetIgnoreCollisions(orbitalCollider);
            }
        }

        private void RemoveOrbital(Orbital orbital)
        {
            orbital.DisableOrbital();
            orbitals.Remove(orbital);
        }

        public Orbital GetOrbital(GameObject orbital)
        {
            Orbital orbitalToReturn = null;
            foreach (var o in orbitals)
            {
                if (o.orbitalObject.Equals(orbital))
                {
                    orbitalToReturn = o;
                }
            }

            return orbitalToReturn;
        }

        public void Update()
        {
            foreach (var orbital in orbitals)
            {
                orbital.UpdateTick();
            }
        }

        public void FixedUpdate()
        {
            foreach (var orbital in orbitals)
            {
                orbital.Tick();
            }
            
            foreach (var orbital in orbitals.Where(orbital => !orbital.Validate()))
            {
                SetCollisions(orbital);
            }
        }
    }
}