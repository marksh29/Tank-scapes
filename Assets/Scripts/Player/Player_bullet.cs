using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] GameObject target;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        speed = Player_stats.Instance.ammo_speed;
    }
    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else 
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<Enemy>().Damage(Player_stats.Instance.ammo_power * (Player_ugrade.Instance.state_id + 1));
            gameObject.SetActive(false);
        }
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
