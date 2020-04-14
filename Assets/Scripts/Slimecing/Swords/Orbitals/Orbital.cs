using System;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    public abstract class Orbital : MonoBehaviour
    {
        [SerializeField] private Collider bodyCollider;
        [SerializeField] private OrbitalStats stats;

        public Collider BodyCollider => bodyCollider;
        public OrbitalStats Stats
        {
            get => stats;
            set => stats = value;
        }
        public GameObject owner { get; set; }
        
        protected bool IsownerNull;
        protected bool IsstatsNull;
        private void Start()
        {
            IsstatsNull = Stats == null;
            IsownerNull = owner == null;
        }
        
        public Orbital SpawnOrbital(TakesOrbitals own)
        {
            var ownerTransform = own.transform;
            Orbital spawnedOrbital = Instantiate(this, ownerTransform.position, Quaternion.identity, ownerTransform);
            return StartOrbital(spawnedOrbital, own);
        }

        public Orbital StartOrbital(Orbital orb, TakesOrbitals own)
        {
            orb.owner = own.gameObject;
            Physics.IgnoreCollision(own.GetComponent<Collider>(), orb.BodyCollider);
            foreach (var orbital in own.spawnedOrbitals)
            {
                Physics.IgnoreCollision(orbital.BodyCollider, orb.BodyCollider);
            }
            return orb;
        }

        private void Update()
        {
            if (!IsownerNull)
            {
                UpdateSword();
            }
        }

        protected virtual void UpdateSword()
        {
            Rotate();
            SetLook();
        }

        protected abstract void Rotate();

        protected abstract void SetLook();
    }
}
