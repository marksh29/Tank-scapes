using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject[] prefabs, enemy;
    [SerializeField] float[] yy;
    [SerializeField] float timer, min_time, max_time;
    [SerializeField] int enemy_count;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy_count = Random.Range(5, 15);
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Spawn();
        }
    }
    void Spawn()
    {
        if(enemy_count > 0)
        {
            enemy_count--;
            timer = Random.Range(min_time, max_time);
            GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]) as GameObject;
            obj.transform.position = new Vector3(yy[Random.Range(0, yy.Length)], 0, player.transform.position.z + 100);
        }
        else
        {
            timer = max_time;
            GameObject obj = Instantiate(enemy[Random.Range(0, enemy.Length)]) as GameObject;
            obj.transform.position = new Vector3(yy[Random.Range(0, yy.Length)], 0, player.transform.position.z + 100);
            enemy_count = Random.Range(5, 15);
            Player_controll.Instance.enemy_attack = true;
        }
    }
}
