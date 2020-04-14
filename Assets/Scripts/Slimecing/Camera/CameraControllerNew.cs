using System.Collections.Generic;
using UnityEngine;

namespace Slimecing.Camera
{
    public class CameraControllerNew : MonoBehaviour {

        public float dampTime = 0.2f;
        public float zoomLimit = 50f;
        public float minZoom = 40f;
        public float maxZoom = 10f;
<<<<<<< HEAD
        
        public float maxDistanceFromOtherPlayers = 70;
        [HideInInspector] public List<Transform> visibleThings = new List<Transform>();
        //private List<Transform> unSeenPlayers = new List<Transform>();
        //private List<float> localDistances = new List<float>();

        private UnityEngine.Camera _cCamera;                
        private Vector3 _cameraVelocity;

        private int _numberPlayers;

        public Vector3 offset;
=======
        public float maxDistanceFromOtherPlayers = 70;

        private List<Transform> visibleThings = new List<Transform>();
        //private List<Transform> unSeenPlayers = new List<Transform>();
        //private List<float> localDistances = new List<float>();
        private UnityEngine.Camera _cCamera;                
        private Vector3 _cameraVelocity;
        private int _numberPlayers;
        public Vector3 offset;

        public List<Transform> VisibleThings { get => visibleThings; set => visibleThings = value; }

>>>>>>> Added triggers and lots of backend
        private void Awake()
        {
            _cCamera = GetComponent<UnityEngine.Camera>();
        }

        private void FixedUpdate()
        {
            Vector3 centerPoint = GetCenterPoint();
            Vector3 desPos = centerPoint + offset;
            transform.position = Vector3.SmoothDamp(transform.position, desPos, ref _cameraVelocity, dampTime);

            float theZoom = Mathf.Lerp(maxZoom, minZoom, FindSize() / zoomLimit);
            _cCamera.fieldOfView = Mathf.Lerp(_cCamera.fieldOfView, theZoom, Time.deltaTime);
        }

        private Vector3 GetCenterPoint()
        {
<<<<<<< HEAD
            switch (visibleThings.Count)
=======
            switch (VisibleThings.Count)
>>>>>>> Added triggers and lots of backend
            {
                case 0:
                    return Vector3.zero;
                case 1:
<<<<<<< HEAD
                    return visibleThings[0].position;
=======
                    return VisibleThings[0].position;
>>>>>>> Added triggers and lots of backend
                default:
                    return FindBoundsAndSetBounds().center;
            }
        }

        private float FindSize()
        {
            return FindBoundsAndSetBounds().size.x;
        }

        private Bounds FindBoundsAndSetBounds()
        {
<<<<<<< HEAD
            if (visibleThings.Count == 0)
=======
            if (VisibleThings.Count == 0)
>>>>>>> Added triggers and lots of backend
            {
                Bounds zeroBounds = new Bounds(Vector3.zero, Vector3.zero);
                return zeroBounds;
           
            }
<<<<<<< HEAD
            Bounds playerBounds = new Bounds(visibleThings[0].position, Vector3.zero);
            foreach (var thing in visibleThings)
=======
            Bounds playerBounds = new Bounds(VisibleThings[0].position, Vector3.zero);
            foreach (var thing in VisibleThings)
>>>>>>> Added triggers and lots of backend
            {
                playerBounds.Encapsulate(thing.position);
            }
            return playerBounds;
        }

        public void AddToCameraList(GameObject thing)
        {
<<<<<<< HEAD
            visibleThings.Add(thing.transform);
        }
        public void RemoveFromCameraList(GameObject thing)
        {
            visibleThings.Remove(thing.transform);
=======
            VisibleThings.Add(thing.transform);
        }
        public void RemoveFromCameraList(GameObject thing)
        {
            VisibleThings.Remove(thing.transform);
>>>>>>> Added triggers and lots of backend
        }
    }
}
