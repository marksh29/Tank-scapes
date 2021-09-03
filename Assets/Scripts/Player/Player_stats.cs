using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{

    public static Player_stats Instance;
    [HeaderAttribute("Управление свайпами")]
    public bool swipe_controll;
    [HeaderAttribute("Автоматическая стрельба танка")]
    public bool auto_fire;
    [HeaderAttribute("Скорость смены полосы при свайпе")]
    public float swipe_speed;
    [HeaderAttribute("Скорость атаки")]
    public float attack_speed;
    [HeaderAttribute("Дистанция начала стрельбы")]
    public float attack_distance;
    [HeaderAttribute("Дистанция до врага для замедления")]
    public float enemy_distance;
    [HeaderAttribute("Скорость движения танка")]
    public float move_speed;
    [HeaderAttribute("Скорость замедления танка")]
    public float move_remove;
    [HeaderAttribute("Скорость движения танка при замедлении")]
    public float frize_move_speed;
    //[HeaderAttribute("Скорость смены полосы")]
    //public float rotate_speed;
    [HeaderAttribute("Скорость скорость убирания башни")]
    public float down_speed;
    [HeaderAttribute("Длина бокового прыжка")]
    public float duble_jump_dist;
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
    [HeaderAttribute("Время полета пули")]
    public float ammo_fly_time;

    [HeaderAttribute("Скорость движения врага")]
    public float enemy_speed;
    [HeaderAttribute("Частота атаки врага")]
    public float enemy_fire_time;
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
