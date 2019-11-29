using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;



public class StartMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject healthAmount;

    public void startGame()
    {
        if (PlayerPrefs.HasKey("health"))
            PublicStatHandler.GetInstance().health = PlayerPrefs.GetInt("health");
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void changeHealth(int amount)
    {
        PublicStatHandler.GetInstance().health += amount;
        if (PublicStatHandler.GetInstance().health > 6)
            PublicStatHandler.GetInstance().health = 6;
        
        else if (PublicStatHandler.GetInstance().health < 1)
            PublicStatHandler.GetInstance().health = 1;
        
        PlayerPrefs.SetInt("health", PublicStatHandler.GetInstance().health);
        updateHealthAmount();
    }

    public void showSettings(bool on)
    {
        settings.SetActive(on);
    }

    public void updateHealthAmount()
    {
        healthAmount.GetComponent<TextMeshProUGUI>().text = PublicStatHandler.GetInstance().health+"";
    }
}
