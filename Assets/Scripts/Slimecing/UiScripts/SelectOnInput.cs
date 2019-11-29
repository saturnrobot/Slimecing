using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    public EventSystem eventSystem;
    public List<GameObject> selectableObjects;

    private bool buttonSelected = false;
    private int cursorPosition = 0;
    private bool lastState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("GlobalHorizontal") != 0)
        {
          //  Debug.Log(Input.GetAxis("GlobalHorizontal"));
        }

        if ((Input.GetAxisRaw("Horizontal") < -0.1 || Input.GetAxis("GlobalHorizontal") < -0.1) && !buttonSelected)
        {
            Debug.Log("Left");
                cursorPosition++;
                if (cursorPosition > selectableObjects.Count - 1)
                {
                    cursorPosition = 0;
                }

                eventSystem.SetSelectedGameObject(selectableObjects[cursorPosition].gameObject);

            buttonSelected = true;

            //GameObject Selected = selectableObjects[cursorPosition].gameObject;
            //ExecuteEvents.Execute<ISelectHandler>(Selected, new BaseEventData(EventSystem.current), ExecuteEvents.selectHandler);

            //buttonSelected = true;
        }

        if ((Input.GetAxisRaw("Horizontal") > 0.1) && !buttonSelected)
        {
            Debug.Log("Right");
                cursorPosition--;
                if (cursorPosition < 0)
                {
                    cursorPosition = 0;
                }

                eventSystem.SetSelectedGameObject(selectableObjects[cursorPosition].gameObject);

            buttonSelected = true;
            //GameObject Selected = selectableObjects[cursorPosition].gameObject;
            //ExecuteEvents.Execute<ISelectHandler>(Selected, new BaseEventData(EventSystem.current), ExecuteEvents.selectHandler);

            //buttonSelected = true;
        }

        if ((Input.GetButtonUp("Horizontal") || LikeOnKeyDown("GlobalHorizontal")) && buttonSelected)
        {
            buttonSelected = false;
        }
    }

    private bool LikeOnKeyDown(string axis)
    {
        //Converts axis input to just true/false like button press
        var currentState = Input.GetAxisRaw(axis) > 0.1;
        if (currentState && lastState)
        {
            return false;
        }

        lastState = currentState;
        //buttonSelected = false;
        return currentState;
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
