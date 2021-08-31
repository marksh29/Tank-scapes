using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
        if (player.position.z - transform.position.z > 250)
            Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
