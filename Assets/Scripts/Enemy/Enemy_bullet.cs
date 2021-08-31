using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bullet : MonoBehaviour
{
    [SerializeField] GameObject expl_other, expl_pl;
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
            GameObject expl = Instantiate(expl_pl, gameObject.transform.position, transform.rotation) as GameObject;
            DestroyObject(expl, 1);
            dead = true;
            Player_hp.Instance.Damage(Player_stats.Instance.enemy_ammo_power);
            StartCoroutine(Off());
            gameObject.SetActive(false);
        }
        else if (coll.gameObject.tag == "Untagged" || coll.gameObject.tag == "Enemy")
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
