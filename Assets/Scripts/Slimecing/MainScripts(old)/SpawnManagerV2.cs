using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerV2 : MonoBehaviour {

    //Player GameObjects
    private GameObject[] slimePlayer;
    private GameObject[] hats;
    private GameObject[] swords;

    public GameObject sword;

    private GameObject spawnedSlime;
    private GameObject spawnedSword;
    private GameObject spawnedHat;
    private GameObject spawnedSwordModel;

    public Camera cam;
    public GameObject dataH;
    public GameObject hud;

    public List<GameObject> spawnedPlayers;
    public List<GameObject> spawnedSwords;

    [HideInInspector] public List<GameObject> existingPlayers;
    //Array of the spawnPoint Transforms
    public List<Transform> spawnPoints;
    private List<int> usableSpawnPoints;
    //Int to hold the number of players in the game
    private int numberPlayers = 4;

    //Int to hold the nubmer of spawn points
    public int numberOfSpawns = 4;

    private WinChecker winChecker;

    private void Start()
    {
        slimePlayer = PublicStatHandler.GetInstance().slimes.ToArray();
        hats = PublicStatHandler.GetInstance().hats.ToArray();
        swords = PublicStatHandler.GetInstance().swords.ToArray();

        usableSpawnPoints = new List<int>();

        existingPlayers = new List<GameObject>();

        numberPlayers = PublicStatHandler.GetInstance().amountOfPlayers;

        Spawn();

        cam.gameObject.SetActive(true);
        dataH.SetActive(true);
        hud.SetActive(true);
        winChecker = dataH.GetComponent<WinChecker>();
    }

    // Use this for initialization
    void Spawn() {

        GameObject[] existingPlayers = FindExistingPlayers("Slime");
        foreach (GameObject slime in existingPlayers)
        {
            Destroy(slime.GetComponent<PlayerMovement>().Sword);
            slime.SetActive(false);
        }
        existingPlayers = null;

        ResetUseableSpawns();

        for (int s = 0; s < numberPlayers; s++) {

            Debug.Log("The number of players is: " + numberPlayers);
            //Spawns player at the random index
            spawnedSlime = (GameObject)Instantiate(slimePlayer[s]);
            spawnedSlime.gameObject.name = "Slime" + (s + 1);
            spawnedSlime.SetActive(true);
            spawnedPlayers.Add(spawnedSlime);

            PlayerMovement playerMov = spawnedSlime.GetComponent<PlayerMovement>();

            if (hats.Length >= (s + 1))
            {
                if (hats[s] != null)
                {
                    spawnedHat = (GameObject)Instantiate(hats[s]);
                    spawnedHat.GetComponent<HatScript>().Owner = spawnedSlime;
                    playerMov.Hat = spawnedHat;
                }
            }

            if (swords.Length >= (s + 1) && swords[s] != null) {
                spawnedSword = (GameObject)Instantiate(swords[s]);
                spawnedSwords.Add(spawnedSword);
                spawnedSword.GetComponent<SwordScript>().Owner = spawnedSlime;
                playerMov.Sword = spawnedSword;
                spawnedSword.gameObject.name = "Sword" + (s + 1);
            }
            else
            {
                spawnedSword = (GameObject)Instantiate(sword);
                spawnedSword.GetComponent<SwordScript>().Owner = spawnedSlime;
                spawnedSwords.Add(spawnedSword);
                playerMov.Sword = spawnedSword;
                spawnedSword.gameObject.name = "Sword" + (s + 1);
            }

            slimePlayer[s].transform.position = SelectSpawnPoint().position;
        }

        if (hud.activeSelf)
        {
            hud.GetComponent<HudAndHealthSetter>().Resetter();
        }

        Respawn();
    }

    public void Respawn()
    {
        ResetUseableSpawns();

        foreach (GameObject slime in FindExistingPlayers("DeadSlime"))
        {
            slime.GetComponent<PlayerMovement>().health = PublicStatHandler.GetInstance().health;
            winChecker.UnSetGhost(slime);
        }

        existingPlayers.Clear();

        existingPlayers.AddRange(FindExistingPlayers("Slime"));
        existingPlayers.AddRange(FindExistingPlayers("DeadSlime"));
        
        foreach (GameObject slime in existingPlayers)
        {
            slime.GetComponent<PlayerMovement>().health = PublicStatHandler.GetInstance().health;
            slime.GetComponent<PlayerMovement>().slimePercentage = 0;
            Debug.Log(slime.name + " " + slime.GetComponent<PlayerMovement>().health);
            slime.transform.position = SelectSpawnPoint().position;
        }

        hud.GetComponent<HudAndHealthSetter>().Resetter();
    }

    public void ResetUseableSpawns()
    {
        usableSpawnPoints.Clear();
        for (int i = 0; i < numberOfSpawns; i++)
        {
            usableSpawnPoints.Add(i);
        }
        Debug.Log("Reset Spawns, Number of spawns at " + usableSpawnPoints.Count);
    }

    public Transform SelectSpawnPoint()
    {
        for (int i = 0; i < usableSpawnPoints.Count; i++)
        {
            Debug.Log("Useable spawnpoints: " + usableSpawnPoints[i]);
        }
        int randomIndex = Random.Range(0, (usableSpawnPoints.Count));
        int randomNumber = (int)usableSpawnPoints[randomIndex];
        Debug.Log("Selected Spawn: " + randomNumber);
        usableSpawnPoints.RemoveAt(randomIndex);
        Debug.Log(spawnPoints[randomNumber].transform.position);
        return spawnPoints[randomNumber];  
    }

    public GameObject[] FindExistingPlayers(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag);
    }
}
