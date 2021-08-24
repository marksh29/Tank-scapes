using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject[] prefabs;
    [SerializeField] float[] yy;
    [SerializeField] float timer, min_time, max_time;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        timer = Random.Range(min_time, max_time);
        GameObject obj = Instantiate(prefabs[Random.Range(0, prefabs.Length)]) as GameObject;
        obj.transform.position = new Vector3(yy[Random.Range(0, yy.Length)], 1, player.transform.position.z + 100);
    }
}
