using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_stats : MonoBehaviour
{
    public static Player_stats Instance;
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
    [HeaderAttribute("Скорость пули")]
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
