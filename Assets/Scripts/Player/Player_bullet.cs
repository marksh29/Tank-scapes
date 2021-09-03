using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    [SerializeField] private float down_time;
    [SerializeField] GameObject expl_other;
    [SerializeField] GameObject[] expl_en;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        down_time = Player_stats.Instance.ammo_fly_time;
    }
    void Update()
    {      
        transform.Translate(Vector3.forward * Player_stats.Instance.ammo_speed * Time.deltaTime);
        down_time -= Time.deltaTime;
        if(down_time <= 0)
        {
            transform.Translate(Vector3.up * -10 * Time.deltaTime);
        }        
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            GameObject expl = Instantiate(expl_en[Player_ugrade.Instance.state_id == 0 ? 0 : Random.Range(0, expl_en.Length)], new Vector3 (transform.position.x, gameObject.transform.position.y + 2, transform.position.z - 1), transform.rotation) as GameObject;
            Object.Destroy(expl, 1);
            coll.gameObject.GetComponent<Enemy>().Damage(Player_stats.Instance.ammo_power);
            gameObject.SetActive(false);
        }
        else 
        {
            GameObject expl = Instantiate(expl_other, transform.position, transform.rotation) as GameObject;
            gameObject.SetActive(false);
            Object.Destroy(expl, 1);
        }
    }
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
