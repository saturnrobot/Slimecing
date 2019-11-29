using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnAwake : MonoBehaviour
{
    public EventSystem events;
    private void OnEnable()
    {
        events.SetSelectedGameObject(gameObject);
        //(StandaloneInputModule)events.currentInputModule
        StandaloneInputModule yourmom=(StandaloneInputModule)events.currentInputModule;
        yourmom.submitButton = "GlobalStart";
    }
}
