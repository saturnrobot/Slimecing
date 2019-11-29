using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudAndHealthSetter : MonoBehaviour
{
    // Start is called before the first frame update
    public float harmNumber, hurtNumber, ouchNumber;
    public List<GameObject> playerHuds;

    public List<GameObject> p1HealthPool, p2HealthPool, p3HealthPool, p4HealthPool;
    public List<GameObject> activeHeartsP1, activeHeartsP2, activeHeartsP3, activeHeartsP4;

    public GameObject spawnManager;
    private SpawnManagerV2 spawnManagementScript;

    private int numPlayers;
    private bool ready = false;

    private bool[] dead;

    void Awake()
    {
        numPlayers = PublicStatHandler.GetInstance().amountOfPlayers;
        Resetter();
        Debug.Log(PublicStatHandler.GetInstance().amountOfPlayers);
        spawnManagementScript = spawnManager.GetComponent<SpawnManagerV2>();
        dead = new bool[PublicStatHandler.GetInstance().amountOfPlayers];
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            for (int i = 0; i < numPlayers; i++)
            {
                if (spawnManagementScript.spawnedPlayers[i].GetComponent<PlayerMovement>().isAlive)
                {
                    dead[i] = false;
                }

                if (spawnManagementScript.spawnedPlayers[i].GetComponent<PlayerMovement>().health < GetHealthList(true, i).Count && !dead[i])
                {
                    Debug.Log(dead[i]);
                    GetHealthList(true, i)[GetHealthList(true, i).Count - 1].SetActive(false);
                    GetHealthList(true, i).RemoveAt(GetHealthList(true, i).Count - 1);

                    if (!spawnManagementScript.spawnedPlayers[i].GetComponent<PlayerMovement>().isAlive)
                    {
                        dead[i] = true;
                    }
                }

                float per = spawnManagementScript.spawnedPlayers[i].GetComponent<PlayerMovement>().slimePercentage;
                if (per < harmNumber) { playerHuds[i].GetComponentInChildren<EmoteChanger>().SetSprite(0); } else
                if (per < hurtNumber) { playerHuds[i].GetComponentInChildren<EmoteChanger>().SetSprite(1); } else
                if (per < ouchNumber) { playerHuds[i].GetComponentInChildren<EmoteChanger>().SetSprite(2); } 
                else { playerHuds[i].GetComponentInChildren<EmoteChanger>().SetSprite(3); }
            }
        }
    }

    public void Resetter()
    {
        ready = false;

        ResetThis(playerHuds);
        ResetThis(p1HealthPool);
        ResetThis(p2HealthPool);
        ResetThis(p3HealthPool);
        ResetThis(p4HealthPool);

        for (int i = 0; i < numPlayers; i++)
        {
            if (GetHealthList(true, i) != null)
            {
                GetHealthList(true, i).Clear();
            }
        }

        SetHealth();
        ready = true;
    }

    public void ResetThis(List<GameObject> theList)
    {
        foreach (GameObject slimeHud in theList)
        {
            slimeHud.SetActive(false);
        }
    }
    public void SetHealth()
    {
        for (int z = 0; z < numPlayers; z++)
        {
            playerHuds[z].SetActive(true);
            playerHuds[z].GetComponentInChildren<EmoteChanger>().SetSprite(0);
        }

        for (int i = 0; i < PublicStatHandler.GetInstance().health; i++)
        {
            for (int j = 0; j < numPlayers; j++)
            {
                GetHealthList(false, j)[i].SetActive(true);
                GetHealthList(true, j).Add(GetHealthList(false, j)[i]);
            }
        }

    }

    private List<GameObject> GetHealthList(bool exist, int index)
    {
        if (exist)
        {
            switch (index)
            {
                case 0: return activeHeartsP1;
                case 1: return activeHeartsP2;
                case 2: return activeHeartsP3;
                case 3: return activeHeartsP4;
                default: return null;
            }
        }
        else
        {
            switch (index)
            {
                case 0: return p1HealthPool;
                case 1: return p2HealthPool;
                case 2: return p3HealthPool;
                case 3: return p4HealthPool;
                default: return null;
            }
        }
    }
}
