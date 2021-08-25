using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{
    public static Player_stats Instance;
    [HeaderAttribute("�������� �����")]
    public float attack_speed;
    [HeaderAttribute("�������� �������� �����")]
    public float move_speed;
    [HeaderAttribute("�������� ����� ������")]
    public float rotate_speed;
    [HeaderAttribute("�������� �������� �������� �����")]
    public float down_speed;
    [HeaderAttribute("�������� �������� ������")]
    public float duble_jump_speed;
    [HeaderAttribute("����������������� ������")]
    public float jump_speed;
    [HeaderAttribute("�������� ����")]
    public float ammo_speed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
       
    }   
}
