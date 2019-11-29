using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public Sprite[] stages;
    public GameObject stageImage;
    public GameObject playerSelect;
    private float nextTime = 0;

    private int currentStage;
    private Image img;

    void Start()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        img = stageImage.GetComponent<Image>();
    }

    void Update()
    {
        if (Input.GetAxis("GlobalHorizontal") >= 0.5 && Time.time > nextTime)
        {
            nextStage();
            nextTime = Time.time + 0.5f;
        }
        else if (Input.GetAxis("GlobalHorizontal") <= -0.5 && Time.time > nextTime)
        {
            lastStage();
            nextTime = Time.time + 0.5f;
        }
    }

    public void lastStage()
    {
        currentStage--;
        if(currentStage < 0)
        {
            currentStage = stages.Length - 1;
        }
        img.sprite = stages[currentStage];
    }

    public void nextStage()
    {
        currentStage++;
        if(currentStage > stages.Length - 1)
        {
            currentStage = 0;
        }
        img.sprite = stages[currentStage];
    }

    public void stageSelected()
    {
        playerSelect.SetActive(true);
        playerSelect.GetComponent<ReadyScript>().stage = currentStage + 2;
        playerSelect.GetComponent<ReadyScript>().stageOff();
    }
}
