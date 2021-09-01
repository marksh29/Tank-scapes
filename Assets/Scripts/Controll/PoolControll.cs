using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControll : MonoBehaviour
{
    public static PoolControll Instance;
    [SerializeField] private GameObject enemy_prefab, bullet_enemy_prefab;
    [SerializeField] private List<GameObject> enemy_stac, en_bullet, pl_bullet_1, pl_bullet_2;
    [SerializeField] GameObject[] new_pos_obj, bullet_player_prefab;
    [SerializeField] Transform[] spawn_pos;
    GameObject new_bullet, obj;
    int diff;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;        
    }    
    void Start()
    {

    } 
    
    public void Restart()
    {
        Elements_off();  
    }
    void Elements_off()
    {
        for (int i = 0; i < enemy_stac.Count; i++)
        {
            enemy_stac[i].SetActive(false);
        }
        for (int i = 0; i < pl_bullet_1.Count; i++)
        {
            pl_bullet_1[i].SetActive(false);
        }
        for (int i = 0; i < pl_bullet_2.Count; i++)
        {
            pl_bullet_2[i].SetActive(false);
        }
        for (int i = 0; i < en_bullet.Count; i++)
        {
            en_bullet[i].SetActive(false);
        }
    }

    //public void Spawn_enemy(int pos_id, int state)
    //{
    //    bool not_empty = false;
    //    for (int i = 0; i < enemy_stac.Count; i++)
    //    {
    //        if (!enemy_stac[i].activeSelf)
    //        {
    //            enemy_stac[i].SetActive(true);
    //            not_empty = true;
    //            enemy_stac[i].GetComponent<Enemy>().Set_state(state);
    //            enemy_stac[i].transform.parent = GameObject.FindGameObjectWithTag("Respawn").transform;
    //            Set_start_position(enemy_stac[i], pos_id);
    //            break;
    //        }
    //    }
    //    if (not_empty == false)
    //    {
    //        GameObject new_enemy = Instantiate(enemy_prefab) as GameObject;
    //        new_enemy.SetActive(true);
    //        enemy_stac.Add(new_enemy);
    //        new_enemy.GetComponent<Enemy>().Set_state(state);
    //        new_enemy.transform.parent = GameObject.FindGameObjectWithTag("Respawn").transform;
    //        Set_start_position(new_enemy, pos_id);
    //    }
    //}
   
   
    public GameObject Spawn_player_bullet_1()
    {
        bool not_empty = false;       
        for (int i = 0; i < pl_bullet_1.Count; i++)
        {
            if (!pl_bullet_1[i].activeSelf)
            {
                pl_bullet_1[i].SetActive(true);
                not_empty = true;
                new_bullet = pl_bullet_1[i];
                break;
            }
        }
        if (not_empty == false)
        {
            GameObject new_obj = Instantiate(bullet_player_prefab[0]) as GameObject;
            pl_bullet_1.Add(new_obj);
            new_bullet = new_obj;
        }
        return new_bullet;
    }
    public GameObject Spawn_player_bullet_2()
    {
        bool not_empty = false;
        for (int i = 0; i < pl_bullet_2.Count; i++)
        {
            if (!pl_bullet_2[i].activeSelf)
            {
                pl_bullet_2[i].SetActive(true);
                not_empty = true;
                new_bullet = pl_bullet_2[i];
                break;
            }
        }
        if (not_empty == false)
        {
            GameObject new_obj = Instantiate(bullet_player_prefab[1]) as GameObject;
            pl_bullet_2.Add(new_obj);
            new_bullet = new_obj;
        }
        return new_bullet;
    }
    public GameObject Spawn_enemy_bullet()
    {
        bool not_empty = false;
        for (int i = 0; i < en_bullet.Count; i++)
        {
            if (!en_bullet[i].activeSelf)
            {
                en_bullet[i].SetActive(true);
                not_empty = true;
                new_bullet = en_bullet[i];
                break;
            }
        }
        if (not_empty == false)
        {
            GameObject new_obj = Instantiate(bullet_enemy_prefab) as GameObject;
            en_bullet.Add(new_obj);
            new_bullet = new_obj;
        }
        return new_bullet;
    }
}
