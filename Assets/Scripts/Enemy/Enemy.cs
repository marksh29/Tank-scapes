using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Image life_image;
    [SerializeField] public bool fire_enemy, bomber, boss_fire;
    [SerializeField] float life, start_life;
    [SerializeField] float damage;
    [SerializeField] GameObject explos_prefab;
    [SerializeField] MeshRenderer[] mesh;
    void Start()
    {
        start_life = life;
    }
    void Update()
    {
        
    }
    public void Damage(float id)
    {
        life -= id;
        
        if (life_image != null)
            life_image.fillAmount = life / start_life;

        if (explos_prefab != null)
            StartCoroutine(Effect_on());
        
        if (life <= 0)
        {
            gameObject.tag = "Untagged";

            for (int i = 0; i < mesh.Length; i++)
            {
                mesh[i].enabled = false;
            }
            Player_controll.Instance.Cleare_enemy();

            if (bomber)
            {
                GetComponent<Bomber>().Dead();
            }
            else if (boss_fire)
            {
                GetComponent<Boss_fire>().Dead();
            }
            else if (fire_enemy)
            {
                GetComponent<Fire_enemy>().Dead();
                //Player_controll.Instance.enemy_attack = false;               
            }               
            else
            {
                Destroy(gameObject, 2);
            }                
        }           
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")// && ((!Player_controll.Instance.jump && fire_pos == "down") || fire_pos == ""))
        {
            if(!Player_controll.Instance.jump && coll.gameObject.tag !=  "Barell")
            {
                Player_hp.Instance.Damage(damage);
                Damage(10);
            }
            if (Player_controll.Instance.jump && fire_enemy)
            {
                //GetComponent<Fire_enemy>().Eojy();
                Damage(10);
            }
            else
                Damage(10);
        }
    }
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }
    IEnumerator Effect_on()
    {        
        explos_prefab.SetActive(true);
        yield return new WaitForSeconds(2);
        explos_prefab.SetActive(false);
    }  
}
