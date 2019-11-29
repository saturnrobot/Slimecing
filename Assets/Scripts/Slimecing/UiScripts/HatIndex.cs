using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatIndex : MonoBehaviour
{
    public Sprite[] images;
    public GameObject[] objects;

    public void Set(int player, GameObject hat)
    {
        PublicStatHandler.GetInstance().hats[player] = hat;
    }

}
