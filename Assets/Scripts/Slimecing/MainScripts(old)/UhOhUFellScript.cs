using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UhOhUFellScript : MonoBehaviour {

    // Use this for initialization
    public GameObject[] slime;
    private bool[] running;
    SpawnManagerV2 spawnManager;

	void Awake()
    {
        Debug.Log("FallScriptStart!!!");
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManagerV2>();
        slime = spawnManager.spawnedPlayers.ToArray();
        running = new bool[PublicStatHandler.GetInstance().amountOfPlayers];
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < PublicStatHandler.GetInstance().amountOfPlayers; i++) {
            if (slime[i].transform.position.y <= -20 && !running[i]) {

                if (AudioManager.getInstance() != null)
                {
                    AudioManager.getInstance().Find("fall").source.Play();
                }

                StartCoroutine(WaitAndRespawn(i, 1f));
                
            }
        }
    }

    private IEnumerator WaitAndRespawn(int i, float respawnTime)
    {
        running[i] = true;
        yield return new WaitForSeconds(respawnTime);

        if (AudioManager.getInstance() != null && !AudioManager.getInstance().Find("fall").source.mute)
        {
            AudioManager.getInstance().Find("fall").source.Stop();
        }

        StartCoroutine(PlayRespawn(0.2f));

        spawnManager.ResetUseableSpawns();
        slime[i].transform.position = spawnManager.SelectSpawnPoint().position;
        slime[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
        slime[i].GetComponent<PlayerMovement>().slimePercentage = 0;
        slime[i].GetComponent<PlayerMovement>().health--;
        running[i] = false;
    }

    private IEnumerator PlayRespawn(float wait)
    {
        yield return new WaitForSeconds(wait);
        if (AudioManager.getInstance() != null)
        {
            AudioManager.getInstance().Find("respawn").source.Play();
        }
    }
}
