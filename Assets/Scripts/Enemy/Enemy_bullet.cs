using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{

    [SerializeField] GameObject expl_other, expl_pl;
    [SerializeField] private float down_time;
    bool dead;
    private void OnEnable()
    {
        dead = false;
        down_time = Player_stats.Instance.ammo_fly_time * 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead)
        {
            transform.Translate(Vector3.forward * Player_stats.Instance.enemy_ammo_speed * Time.deltaTime);
            down_time -= Time.deltaTime;
            if (down_time <= 0)
            {
                transform.Translate(Vector3.up * -10 *  Time.deltaTime);
            }
        }           
    }
    private void OnTriggerEnter(Collider coll)
    {        

        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Finish")
        {
            GameObject expl = Instantiate(expl_pl, gameObject.transform.position, transform.rotation) as GameObject;
            DestroyObject(expl, 1);
            dead = true;
            Player_hp.Instance.Damage(Player_stats.Instance.enemy_ammo_power);
            StartCoroutine(Off());
            gameObject.SetActive(false);
        }
        else if (coll.gameObject.tag == "Untagged")
        {
            GameObject expl = Instantiate(expl_other, transform.position, transform.rotation) as GameObject;
            DestroyObject(expl, 1);
            gameObject.SetActive(false);
        }       
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
