using System.Collections.Generic;
using UnityEngine;

namespace Slimecing.Camera
{
    public class CameraControllerNew : MonoBehaviour {

        public float dampTime = 0.2f;
        public float zoomLimit = 50f;
        public float minZoom = 40f;
        public float maxZoom = 10f;
        
        public float maxDistanceFromOtherPlayers = 70;
        [HideInInspector] public List<Transform> visibleThings = new List<Transform>();
        //private List<Transform> unSeenPlayers = new List<Transform>();
        //private List<float> localDistances = new List<float>();

        private UnityEngine.Camera _cCamera;                
        private Vector3 _cameraVelocity;

        private int _numberPlayers;

        public Vector3 offset;
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
            switch (visibleThings.Count)
            {
                case 0:
                    return Vector3.zero;
                case 1:
                    return visibleThings[0].position;
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
            if (visibleThings.Count == 0)
            {
                Bounds zeroBounds = new Bounds(Vector3.zero, Vector3.zero);
                return zeroBounds;
           
            }
            Bounds playerBounds = new Bounds(visibleThings[0].position, Vector3.zero);
            foreach (var thing in visibleThings)
            {
                playerBounds.Encapsulate(thing.position);
            }
            return playerBounds;
        }

        public void AddToCameraList(GameObject thing)
        {
            visibleThings.Add(thing.transform);
        }
        public void RemoveFromCameraList(GameObject thing)
        {
            visibleThings.Remove(thing.transform);
        }
    }
}
