using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] float damage;
    [SerializeField] string fire_pos;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Damage(int id)
    {
        life -= id * Player_stats.Instance.attack;
        if (life <= 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && ((fire_pos == "up" && !Player_controll.Instance.down) || (fire_pos == "down" && !Player_controll.Instance.jump)))
        {
            Player_hp.Instance.Damage(damage);
            Destroy(gameObject);
        }
    }
}
