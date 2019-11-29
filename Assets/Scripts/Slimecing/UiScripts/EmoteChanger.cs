using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteChanger : MonoBehaviour
{
    private GameObject activeObject;

    // Update is called once per frame
    public void SetSprite(int index) {
        ClearSprite();
        switch (index) {
            case 0:
                activeObject = transform.GetChild(0).gameObject;
                break;
            case 1:
                activeObject = transform.GetChild(1).gameObject;
                break;
            case 2:
                activeObject = transform.GetChild(2).gameObject;
                break;
            case 3:
                activeObject = transform.GetChild(3).gameObject;
                break;
            case 4:
                activeObject = transform.GetChild(4).gameObject;
                break;
            case 5:
                activeObject = transform.GetChild(5).gameObject;
                break;
            case 6:
                activeObject = transform.GetChild(6).gameObject;
                break;
            default:
                activeObject = transform.GetChild(7).gameObject;
                break;
        }
        if (activeObject != null) {
            activeObject.SetActive(true);
        }
    }

    public void ClearSprite() {
        if (activeObject != null) {
            activeObject.SetActive(false);
        }
    }
}
