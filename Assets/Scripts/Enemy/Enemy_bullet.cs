using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{
    bool dead;
    private void OnEnable()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
            transform.Translate(Vector3.forward * Player_stats.Instance.enemy_ammo_speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider coll)
    {        
        if (coll.gameObject.tag == "Player")
        {
            print(coll.gameObject.tag);
            dead = true;
            Player_hp.Instance.Damage(Player_stats.Instance.enemy_ammo_power);
            StartCoroutine(Off());
        }
        gameObject.SetActive(false);
    }
    IEnumerator Off()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        transform.GetChild(0).gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
