using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Image life_image;
    [SerializeField] bool fire_enemy, bomber;
    [SerializeField] float life, start_life;
    [SerializeField] float damage;
    [SerializeField] string fire_pos;
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

        StartCoroutine(Effect_on());
        if (life <= 0)
        {
            if (fire_enemy)
                GetComponent<Fire_enemy>().dead = true;

            gameObject.tag = "Untagged";
            for (int i = 0; i < mesh.Length; i++)
            {
                mesh[i].enabled = false;
            }
            Player_controll.Instance.Cleare_enemy(gameObject);
            if (bomber)
            {
                GetComponent<Bomber>().Dead();
            }                
            if (fire_enemy)
            {
                Player_controll.Instance.enemy_attack = false;
                GetComponent<Fire_enemy>().Dead();
            }               
            else
            {
                Destroy(gameObject, explos_prefab != null ? 1 : 0);
            }                
        }           
    }
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "Player")// && ((!Player_controll.Instance.jump && fire_pos == "down") || fire_pos == ""))
        {           
            if (bomber)
            {

                GetComponent<Bomber>().Dead();
            }
            if (fire_enemy)
            {

                GetComponent<Fire_enemy>().Dead();
            }               
            else
            {
                Player_hp.Instance.Damage(damage);
                Player_ugrade.Instance.Update_tank(-1);
                Damage(10);
            }
        }
    }
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
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
