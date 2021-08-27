using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ugrade : MonoBehaviour
{
    public static Player_ugrade Instance;

    public int state_id;
    int color_id;

    public MeshRenderer[] up;
    public MeshRenderer[] down;

    public Material[] tank_1;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        color_id = PlayerPrefs.GetInt("tank_color");
        Set_color();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            color_id = 0;
            Set_color();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            color_id = 1;
            Set_color();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            color_id = 2;
            Set_color();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            color_id = 3;
            Set_color();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            color_id = 4;
            Set_color();
        }
    }
    void Set_color()
    {
        for (int i = 0; i < up.Length; i++)
        {
            //up[i].material = tank_1[color_id];
            //down[i].material = tank_1[color_id];
            up[i].materials[state_id == 0 ? 1 : 0] = tank_1[color_id];
            down[i].materials[state_id == 0 ? 1 : 0] = tank_1[color_id];
        }
    }
    public void Update_tank(int id)
    {
        if((id > 0 && state_id <2) || (id < 0 && state_id > 0))
            state_id += id;

        for (int i = 0; i < up.Length; i++)
        {
            up[i].gameObject.SetActive(i == state_id ? true : false);
            down[i].gameObject.SetActive(i == state_id ? true : false);
        }
    }
}
