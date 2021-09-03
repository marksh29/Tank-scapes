using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{

    public static Player_stats Instance;
    [HeaderAttribute("���������� ��������")]
    public bool swipe_controll;
    [HeaderAttribute("�������������� �������� �����")]
    public bool auto_fire;
    [HeaderAttribute("�������� ����� ������ ��� ������")]
    public float swipe_speed;
    [HeaderAttribute("�������� �����")]
    public float attack_speed;
    [HeaderAttribute("��������� ������ ��������")]
    public float attack_distance;
    [HeaderAttribute("��������� �� ����� ��� ����������")]
    public float enemy_distance;
    [HeaderAttribute("�������� �������� �����")]
    public float move_speed;
    [HeaderAttribute("�������� ���������� �����")]
    public float move_remove;
    [HeaderAttribute("�������� �������� ����� ��� ����������")]
    public float frize_move_speed;
    //[HeaderAttribute("�������� ����� ������")]
    //public float rotate_speed;
    [HeaderAttribute("�������� �������� �������� �����")]
    public float down_speed;
    [HeaderAttribute("����� �������� ������")]
    public float duble_jump_dist;
    [HeaderAttribute("�������� �������� ������")]
    public float duble_jump_speed;
    [HeaderAttribute("����������������� ������")]
    public float jump_speed;
    [HeaderAttribute("�������� �������� �����")]
    public float up_speed;
    [HeaderAttribute("�������� ����")]
    public float ammo_speed;
    [HeaderAttribute("���� ����� ����")]
    public float ammo_power;
    [HeaderAttribute("����� ������ ����")]
    public float ammo_fly_time;

    [HeaderAttribute("�������� �������� �����")]
    public float enemy_speed;
    [HeaderAttribute("������� ����� �����")]
    public float enemy_fire_time;
    [HeaderAttribute("���� ����� ���� �����")]
    public float enemy_ammo_power;
    [HeaderAttribute("�������� �������� ��������� ����")]
    public float enemy_ammo_speed;
    [HeaderAttribute("�������� ����� ������")]
    public float enemy_change_speed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
       
    }   
}
