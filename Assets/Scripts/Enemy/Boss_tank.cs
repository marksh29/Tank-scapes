using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_tank : MonoBehaviour
{
    bool dead;
    GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Vector3 targetDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5) - gameObject.transform.position;
            float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection, singleStep, 0.0f);
            gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
        }
        
    }
}
