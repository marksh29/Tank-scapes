using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] float damage;
    [SerializeField] string fire_pos;
    [SerializeField] GameObject explos_prefab;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Damage(int id)
    {
        life -= id;
        if (life <= 0)
        {
            if (explos_prefab != null)
                explos_prefab.SetActive(true);
            Destroy(gameObject, explos_prefab != null ? 1 : 0);
        }
           
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && ((fire_pos == "up" && !Player_controll.Instance.down) || (fire_pos == "down" && !Player_controll.Instance.jump) || fire_pos == ""))
        {
            Damage(10);
            Player_hp.Instance.Damage(damage);            
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
