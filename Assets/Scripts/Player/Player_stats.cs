using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{
    public static Player_stats Instance;   
    public int attack;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        attack = 1;
    }   
}
