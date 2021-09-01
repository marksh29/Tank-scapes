using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    [SerializeField] GameObject expl_prefab, player;
    [SerializeField] float speed, attack_dist;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }    
    void Update()
    {
        Vector3 targetDirection = player.transform.position - gameObject.transform.position;
        float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection, singleStep, 0.0f);
        gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
        if (transform.position.z - player.transform.position.z < attack_dist)
        {
            GetComponent<Animator>().enabled = true;
        }

    }
    public void Dead()
    {
        GetComponent<Animator>().enabled = false;
    }
}
