using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] float damage;
    [SerializeField] string fire_pos;
    [SerializeField] GameObject explos_prefab;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void Damage(float id)
    {
        life -= id;
        StartCoroutine(Effect_on());
        if (life <= 0)
        {
            gameObject.tag = "Untagged";                  
            Destroy(gameObject, explos_prefab != null ? 1 : 0);
            Player_controll.Instance.Cleare_enemy(gameObject);
        }           
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player" && ((fire_pos == "up" && !Player_controll.Instance.down) || (fire_pos == "down" && !Player_controll.Instance.jump) || fire_pos == ""))
        {
            Damage(10);
            Player_hp.Instance.Damage(damage);
            Player_ugrade.Instance.Update_tank(-1);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    IEnumerator Effect_on()
    {
        if (explos_prefab != null)
            explos_prefab.SetActive(true);
        yield return new WaitForSeconds(2);
        if (explos_prefab != null)
            explos_prefab.SetActive(false);
    }
}
