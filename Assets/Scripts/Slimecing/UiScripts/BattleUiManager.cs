using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleUiManager : MonoBehaviour {

    private Canvas winnerScreen;
    private Canvas cardHud;
    private SpawnManagerV2 spawnManager;

    private string oldWinText;

    private void Start()
    {
        winnerScreen = transform.GetChild(0).GetComponent<Canvas>();
        oldWinText = winnerScreen.transform.GetChild(0).GetComponent<Text>().text;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerV2>();
    }

    public void Winner(string winner)
    {
        Text winnerText = winnerScreen.transform.GetChild(0).GetComponent<Text>();
        winnerText.text = winner + winnerText.text;
        winnerScreen.gameObject.SetActive(true);
        Debug.Log("Actually");
    }

    public void RespawnButton()
    {
        winnerScreen.GetComponentInChildren<Text>().text = oldWinText;
        winnerScreen.gameObject.SetActive(false);
        spawnManager.Respawn();
    }
    public void MainMenuButton()
    {
        winnerScreen.GetComponentInChildren<Text>().text = oldWinText;
        winnerScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
