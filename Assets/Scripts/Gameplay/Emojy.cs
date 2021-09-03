using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emojy : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    }
}
