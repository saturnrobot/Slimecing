using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PublicStatHandler : MonoBehaviour {

    public PlayerInputs[] Inputs;
    public List<int> controllers;
    public int amountOfPlayers = 3;
    public int health = 1;
    public List<GameObject> slimes;
    public List<GameObject> hats;
    public List<GameObject> swords;
    public List<GameObject> colours;

    public static PublicStatHandler instance;

    void Awake()
    {
        
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public static PublicStatHandler GetInstance()
    {
        return instance;
    }
}
