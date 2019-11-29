using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float dampTime = 0.2f;
    public float zoomLimit = 50f;
    public float minZoom = 40f;
    public float maxZoom = 10f;

    [SerializeField] private bool newCamera;

    public float maxDistanceFromOtherPlayers = 70;
    /*[HideInInspector]*/ public List<Transform> players = new List<Transform>();
    //private List<Transform> unSeenPlayers = new List<Transform>();
    //private List<float> localDistances = new List<float>();

    private Camera cCamera;                
    private Vector3 cameraVelocity;

    SpawnManagerV2 spawnManager;

    //public bool CameraOn;

    private int numberPlayers;

    public Vector3 offset;
    private void Awake()
    {
        if (!newCamera)
        {
            Debug.Log("CameraStart!!!");
            spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerV2>();
            numberPlayers = PublicStatHandler.GetInstance().amountOfPlayers;
        }

        cCamera = GetComponentInChildren<Camera>();

        for (int i = 0; i < numberPlayers; i++)
        {
            Debug.Log(spawnManager.spawnedPlayers[i].transform);
            players.Add(spawnManager.spawnedPlayers[i].gameObject.transform);
        }
    }

    private void FixedUpdate()
    {
        /*foreach (Transform slimesTransform in players)
        {
            foreach (Transform slimesTransform2 in players)
            {
                float localdist = Vector3.Distance(slimesTransform.position, slimesTransform2.position);
                localDistances.Add(localdist);
            }

            localDistances.Sort();
            //Debug.Log(localDistances[players.Count - 1]);
            distances.Add(localDistances[players.Count - 1]);
            localDistances.Clear();
        }

        distances.Sort();
        if (distances[distances.Count - 1] > maxDistanceFromOtherPlayers)
        {
            Debug.Log("OutOfBounds");
        }
        distances.Clear();*/

        Vector3 centerPoint = GetCenterPoint();
        Vector3 desPos = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desPos, ref cameraVelocity, dampTime);

        float theZoom = Mathf.Lerp(maxZoom, minZoom, FindSize() / zoomLimit);
        cCamera.fieldOfView = Mathf.Lerp(cCamera.fieldOfView, theZoom, Time.deltaTime);
    }

    private Vector3 GetCenterPoint()
    {
        switch (players.Count)
        {
            case 0:
                return Vector3.zero;
            case 1:
                return players[0].position;
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
        if (players.Count == 0)
        {
            Bounds zeroBounds = new Bounds(Vector3.zero, Vector3.zero);
            return zeroBounds;
           
        }
        Bounds playerBounds = new Bounds(players[0].position, Vector3.zero);
        foreach (var slimes in players)
        {
            playerBounds.Encapsulate(slimes.position);
        }
        return playerBounds;
    }

    public void AddPlayerToPlayerToCameraList(GameObject player)
    {
        
    }
}
