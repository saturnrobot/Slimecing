using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBaldwin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slime"))
        {
            if (AudioManager.getInstance() != null)
            {
                AudioManager.getInstance().Find("baldwin").source.Play();
            }
        }
    }
}
