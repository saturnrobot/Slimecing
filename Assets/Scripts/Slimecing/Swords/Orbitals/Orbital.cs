using System;
using System.Collections.Generic;
using Slimecing.Dependency;
using Slimecing.Swords.DropBehaviour;
using UnityEditor;
using UnityEngine;

namespace Slimecing.Swords.Orbitals
{
    [Serializable]
    public class Orbital
    {
        
        [SerializeField] private GameObject owner;
        [SerializeField] private GameObject orbital;
        [SerializeField] private OrbitalLogic orbitalLogic;
        [SerializeField] private float orbitalSpeed;
        [SerializeField] private float yOffset;
        [SerializeField] private float xAxis;
        [SerializeField] private float yAxis;
        [SerializeField] private bool sticky;
        [SerializeField] private LayerMask dropLayer;

        private List<Collider> _ignoredCollisions;
        private bool _hasDropBehaviour;

        private GameObject _cachedOwner;
        private GameObject _cachedOrbital;
        private OrbitalLogic _cachedOrbitalLogic;
        public Collider orbitalCollider { get; private set; }
        public Collider ownerCollider { get; private set; }
        public float orbitalProgress { get; set; }
        public Ellipse orbitPath { get; set; }
        public float rotationSpeed
        {
            get => orbitalSpeed;
            set => orbitalSpeed = value;
        }

        public float verticalOffset
        {
            get => yOffset;
            set => yOffset = value;
        }

        public float radiusX
        {
            get => xAxis;
            set => xAxis = value;
        }

        public float radiusY
        {
            get => yAxis;
            set => yAxis = value;
        }
        
        public bool orbitalSticky
        {
            get => sticky;
            set => sticky = value;
        }

        public GameObject ownerObject
        {
            get => owner;
            set => owner = value;
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

        public Orbital()
        {
            orbitPath = new Ellipse(radiusX, radiusY);
        }

        public Orbital(GameObject owner, GameObject orbital, OrbitalLogic orbitalLogic, float orbitalSpeed, 
            float yOffset, float xAxis, float yAxis, bool sticky)
        {
            this.owner = owner;
            this.orbital = orbital;
            this.orbitalLogic = orbitalLogic;
            this.orbitalSpeed = orbitalSpeed;
            this.yOffset = yOffset;
            this.xAxis = xAxis;
            this.yAxis = yAxis;
            this.sticky = sticky;
            orbitPath = new Ellipse(radiusX, radiusY);
        }

        public void Initialize()
        {
            orbitPath = new Ellipse(radiusX, radiusY);
            InitializeOrbitalObject();
            InitializeOrbitalLogic();
        }

        private void InitializeOrbitalObject()
        {
            SetCollider();
            if (orbitalObject.GetComponent<DropLogic>() != null) _hasDropBehaviour = true;
            _cachedOrbital = orbitalObject;
            _cachedOwner = ownerObject;
        }

        private void InitializeOrbitalLogic()
        {
            currentOrbitalLogic = currentOrbitalLogic.GetOrbital();
            _cachedOrbitalLogic = currentOrbitalLogic;
            currentOrbitalLogic.Initialize(this);
        }

        private void SetCollider()
        {
            orbitalCollider = orbital.gameObject.GetComponent<Collider>();
            ownerCollider = owner.gameObject.GetComponent<Collider>();
            if (ownerCollider == null || orbitalCollider == null) return;
            SetIgnoreCollisions(ownerCollider);
        }

        public void SetIgnoreCollisions(Collider toIgnore)
        {
            if (_ignoredCollisions == null)
            {
                _ignoredCollisions = new List<Collider>();
            }
            Physics.IgnoreCollision(orbitalCollider, toIgnore);
            _ignoredCollisions.Add(toIgnore);
        }

        public void UpdateTick()
        {
            if (currentOrbitalLogic is IOrbitalTickEveryFrame inputOrbitalLogic)
            {
                inputOrbitalLogic.TickUpdate(this);
            }
        }
        public void Tick()
        {
            currentOrbitalLogic.Tick(this);
        }

        public bool Validate()
        {
            bool isValid = true;
            if (_cachedOrbital != orbitalObject || _cachedOwner != ownerObject)
            {
                DisableOrbital();
                InitializeOrbitalObject();
                isValid = false;
            }

            if (_cachedOrbitalLogic != currentOrbitalLogic)
            {
                InitializeOrbitalLogic();
                isValid = false;
            }

            return isValid;
        }

        public void DisableOrbital()
        {
            if (_cachedOrbital == null) return;
            _cachedOrbital.layer = dropLayer;
            foreach (var col in _ignoredCollisions)
            {
                Physics.IgnoreCollision(orbitalCollider, col,false);
            }

            if (_hasDropBehaviour)
            {
                _cachedOrbital.GetComponent<DropLogic>().Drop();
            }
        }
    }
}
