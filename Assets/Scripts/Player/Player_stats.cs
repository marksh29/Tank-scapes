using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{

    public static Player_stats Instance;
    [HeaderAttribute("Управление свайпами")]
    public bool swipe_controll;
    [HeaderAttribute("Скорость смены полосы при свайпе")]
    public float swipe_speed;
    [HeaderAttribute("Скорость атаки")]
    public float attack_speed;
    [HeaderAttribute("Скорость движения танка")]
    public float move_speed;
    [HeaderAttribute("Скорость смены полосы")]
    public float rotate_speed;
    [HeaderAttribute("Скорость скорость убирания башни")]
    public float down_speed;
    [HeaderAttribute("Скорость бокового прыжка")]
    public float duble_jump_speed;
    [HeaderAttribute("Продолжительность прыжка")]
    public float jump_speed;
    [HeaderAttribute("Скорость поворота башни")]
    public float up_speed;
    [HeaderAttribute("Скорость пули")]
    public float ammo_speed;
    [HeaderAttribute("Сила атаки пули")]
    public float ammo_power;

    [HeaderAttribute("Скорость движения врага")]
    public float enemy_speed;
    [HeaderAttribute("Сила атаки пули врага")]
    public float enemy_ammo_power;
    [HeaderAttribute("Скорость движения вражеской пули")]
    public float enemy_ammo_speed;
    [HeaderAttribute("Скорость смены полосы")]
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
