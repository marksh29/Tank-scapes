using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] GameObject target, expl_other;
    [SerializeField] GameObject[] expl_en;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        speed = Player_stats.Instance.ammo_speed;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Player_stats.Instance.ammo_speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider coll)
    {
        print(coll.gameObject.tag);
        if(coll.gameObject.tag == "Enemy")
        {
            GameObject expl = Instantiate(expl_en[Player_ugrade.Instance.state_id == 0 ? 0 : Random.Range(0, expl_en.Length)], new Vector3 (transform.position.x, gameObject.transform.position.y + 2, transform.position.z - 1), transform.rotation) as GameObject;
            DestroyObject(expl, 1);
            coll.gameObject.GetComponent<Enemy>().Damage(Player_stats.Instance.ammo_power);
            gameObject.SetActive(false);
        }
        else if(coll.gameObject.tag == "Untagged")
        {
            GameObject expl = Instantiate(expl_other, transform.position, transform.rotation) as GameObject;
            DestroyObject(expl, 1);
            gameObject.SetActive(false);
        }
    }
    private void OnBecameInvisible()
    {
        Player_controll.Instance.enemy_attack = false;
        gameObject.SetActive(false);
    }
}
