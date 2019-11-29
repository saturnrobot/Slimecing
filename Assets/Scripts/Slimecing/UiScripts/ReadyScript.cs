using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadyScript : MonoBehaviour
{
    public int stage;
    public GameObject stageSelect;

    public void loadScene()
    {
        if (PlayerPrefs.HasKey("health"))
            PublicStatHandler.GetInstance().health = PlayerPrefs.GetInt("health");
        SceneManager.LoadScene(stage);
    }

    public void stageOff()
    {
        stageSelect.SetActive(false);
    }
}
