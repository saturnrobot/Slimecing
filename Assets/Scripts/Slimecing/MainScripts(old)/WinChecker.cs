using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour {

    // Use this for initialization
    private List<GameObject> existingPlayers;
    private List<Material> deadBoisMaterials;
    private List<Material> deadBoisPMaterials;
    private GameObject[] slimePlayers;
    public Material deadMat;
    public GameObject battleUiObject;
    private BattleUiManager battleUiManager;

    private void Awake()
    {
        Debug.Log("WinScriptStart!!!");

        //battleUiManager = GameObject.Find("Ui").GetComponent<BattleUiManager>();
        battleUiManager = battleUiObject.GetComponent<BattleUiManager>();
        //GOING TO NEED TO MAKE A SCRIPT THAT JUST FINDS EXISTING PLAYERS ON START SO THIS DOES NOT HAVE TO BE DONE AGAIN AND AGAIN
        deadBoisMaterials = new List<Material>();
        deadBoisPMaterials = new List<Material>();

        slimePlayers = new GameObject[PublicStatHandler.GetInstance().amountOfPlayers];

        slimePlayers = PublicStatHandler.GetInstance().slimes.ToArray();

        for (int i = 0; i < slimePlayers.Length; i++)
        {
            GameObject body = slimePlayers[i].transform.GetChild(0).transform.GetChild(2).gameObject;
            Debug.Log(deadBoisMaterials.Count);
            if (body.GetComponent<Renderer>().sharedMaterial != null)
            {
                Debug.Log("Found Material");
                deadBoisMaterials.Add(body.GetComponent<Renderer>().sharedMaterial);
            }
            if (slimePlayers[i].GetComponentInChildren<ParticleSystem>() != null)
            {
                deadBoisPMaterials.Add(slimePlayers[i].transform.GetChild(2).gameObject.GetComponent<Renderer>().sharedMaterial);
            }
            Debug.Log("Done " + deadBoisMaterials.Count);
        }

        for (int i = 0; i < deadBoisMaterials.Count; i++)
        {
            Debug.Log(deadBoisMaterials[i]); 
        }
    }

    public void Dead(GameObject deadBoi)
    {
        SetGhost(deadBoi);
        CheckPlayer(deadBoi);
    }

	public void CheckPlayer(GameObject deadBoi)
    {
        GameObject[] existingPlayersArray = GameObject.FindGameObjectsWithTag("Slime");
        existingPlayers = new List<GameObject>();
        existingPlayers.AddRange(existingPlayersArray);
        existingPlayers.Remove(deadBoi);
        if (existingPlayers.Count == 1)
        {
            deadBoi.GetComponent<PlayerMovement>().slimePercentage = 0;
            string returnString = existingPlayers[0].name.Replace("Slime", "P");
            battleUiManager.Winner(returnString + " ");
        }
        Debug.Log(existingPlayers);
    }

    public void SetGhost(GameObject deadBoi)
    {
        deadBoi.tag = "DeadSlime";
        deadBoi.layer = 15;
        deadBoi.GetComponent<PlayerMovement>().Sword.SetActive(false);
        deadBoi.transform.GetChild(1).gameObject.SetActive(false);
        GameObject body = deadBoi.transform.GetChild(0).transform.GetChild(2).gameObject;
        body.GetComponent<Renderer>().sharedMaterial = deadMat;
        deadBoi.transform.GetChild(2).gameObject.GetComponent<Renderer>().sharedMaterial = deadMat;
    }

    public void UnSetGhost(GameObject aliveBoi)
    {
        aliveBoi.tag = "Slime";
        aliveBoi.layer = 9;
        aliveBoi.GetComponent<PlayerMovement>().isAlive = true;
        aliveBoi.GetComponent<PlayerMovement>().Sword.SetActive(true);
        aliveBoi.transform.GetChild(1).gameObject.SetActive(true);
        int playerNumber = 0;
        if(int.TryParse(aliveBoi.name.Replace("Slime", ""), out playerNumber))
        {
            playerNumber -= 1;
        }
        
        if (deadBoisMaterials.Count - playerNumber < 0)
        {
            playerNumber = 0;
        }
        GameObject body = aliveBoi.transform.GetChild(0).transform.GetChild(2).gameObject;
        body.GetComponent<Renderer>().sharedMaterial = deadBoisMaterials[playerNumber];
        aliveBoi.transform.GetChild(2).gameObject.GetComponent<Renderer>().sharedMaterial = deadBoisPMaterials[playerNumber];

    }
}
