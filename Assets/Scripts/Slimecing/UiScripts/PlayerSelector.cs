using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour {

    public GameObject button;
    public GameObject Stats;
    public Text[] playerTexts;
    public GameObject[] playerObjects;
    public ListSelector[] playerHats;

    public List<int> addedControllers;
    private int playerIn = 0;
    private string[] controllers = {"Keyboard", "Joystick 1", "Joystick 2", "Joystick 3", "Joystick 4"};
    private bool[] controllerIn = {false, false, false, false, false};


    private List<string> playControllers;

    // Update is called once per frame
    void Update () {
        if(playerIn >= 1)
        {
            button.SetActive(true);
        }

		if (Input.GetButtonDown("Submit") && !controllerIn[0])
        {
            Debug.Log("Keyboard in");
            controllerIn[0] = true;
            addedControllers.Add(0);
            ShowText("Keyboard In!");
            AddPlayer();
            AddController(playerIn, 0);
            PublicStatHandler.GetInstance().amountOfPlayers = playerIn;
            PublicStatHandler.GetInstance().hats.Add(null);
            //Debug.Log(PSH.controllers.Count);
        }

        for (int i = 1; i < 5; i++)
        {
            if(Input.GetButtonDown("jSubmit" + i) || Input.GetButtonDown("jStart" + i))
            {
                if (!controllerIn[i])
                {
                    Debug.Log(controllers[i] + " in");
                    controllerIn[i] = true;
                    addedControllers.Add(i);
                    ShowText(controllers[i] + " In!");
                    AddPlayer();
                    AddController(playerIn, i);
                    playerHats[playerIn-1].controller = i-1;
                    PublicStatHandler.GetInstance().amountOfPlayers = playerIn;
                    PublicStatHandler.GetInstance().hats.Add(null);
                    //Debug.Log(PSH.controllers.Count);
                }
            }
        }

	}

    private void AddController(int player, int index)
    {
        PublicStatHandler.GetInstance().slimes[player - 1].GetComponent<PlayerMovement>().setUpControls(index);
    } 

    private void ShowText(string infoText)
    {
        if (playerIn <= 4)
        {
            playerTexts[playerIn].gameObject.transform.GetChild(0).GetComponent<Text>().text = infoText;
            playerTexts[playerIn].gameObject.SetActive(true);
            playerObjects[playerIn].gameObject.SetActive(true);
        }
    }

    private void AddPlayer()
    {
        if (playerIn <= 4)
        {
            playerIn++;
        }
    }
    public void StartGame()
    {

    }
}
