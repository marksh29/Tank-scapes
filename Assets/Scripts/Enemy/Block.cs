using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float damage;
    void Start()
    {
        
    }
   
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && !Player_controll.Instance.up_jump && !Player_controll.Instance.jump)
        {
            Player_hp.Instance.Damage(damage);
            Player_controll.Instance.Block();
        }
    }
}
