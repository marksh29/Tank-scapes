using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_enemy : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject player, fire_position;
    [SerializeField] int cur_pos;
    [SerializeField] float[] min_max_pos;
    [SerializeField] float speed, fire_timer, move_timer, bullet_start_pos;
    public bool stay, dead, fire, end;
    [SerializeField] GameObject[] emojy;

    private void OnEnable()
    {
        //anim.SetTrigger("move");
        player = GameObject.FindGameObjectWithTag("Player");
        cur_pos = Random.Range(0, min_max_pos.Length);
        transform.position = new Vector3(min_max_pos[cur_pos], transform.position.y, transform.position.z);
        fire_timer = Random.Range(1f, 3f);
        move_timer = Random.Range(1f, 2f);
    }
    void Start()
    {
               
    }
    void Update()
    {
        if(!dead)
        {
            if(!stay)
            {
                transform.Translate(Vector3.forward * Player_stats.Instance.enemy_speed * Time.deltaTime);

                move_timer -= Time.deltaTime;
                if (move_timer <= 0)
                    Change_move();

                //if (transform.position.z < player.transform.position.z)
                //{
                //   StartCoroutine(Full_stop());
                //}

                fire_timer -= Time.deltaTime;
                if (fire_timer <= 0 && (transform.position.z - player.transform.position.z < Player_stats.Instance.attack_distance))
                {
                    StopAllCoroutines();
                    StartCoroutine(Fire_on());
                }                    
            }
            else
            {
                    Vector3 targetDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5) - gameObject.transform.position;
                    float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
                    Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection, singleStep, 0.0f);
                    gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
            }

            if(transform.position.z < player.transform.position.z && !end)
            {
                Player_controll.Instance.Cleare_enemy();
                end = true;
                transform.LookAt(player.transform);
                StopAllCoroutines();
                StartCoroutine(Fire_on());
            }
        }
    }
    //IEnumerator Full_stop()
    //{
    //    stay = true;
    //    Player_controll.Instance.Full_stop();
    //    // -- добавить анимации
    //    yield return new WaitForSeconds(1);
    //    Player_hp.Instance.Damage(1);
    //}
    void Change_move()
    {
        move_timer = Random.Range(2f, 5f);
        //if (cur_pos == 0)
        //    cur_pos = 1;
        //else if (cur_pos == 4)
        //    cur_pos = 3;
        //else
        //    cur_pos = Random.Range(cur_pos - 1, cur_pos + 1);

        float pos = Random.Range(min_max_pos[0], min_max_pos[min_max_pos.Length - 1]);
        float time = Mathf.Abs(transform.position.x - pos) / 3;
        StartCoroutine(DoMove(time / Player_stats.Instance.enemy_change_speed, pos));
    }
    private IEnumerator DoMove(float time, float xx)
    {
        Vector2 startPosition = transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            transform.position = Vector3.Lerp(new Vector3(startPosition.x, transform.position.y, transform.position.z), new Vector3(xx, transform.position.y, transform.position.z), fraction);
            yield return null;
        }
    }
    IEnumerator Fire_on()
    {
        anim.SetTrigger("fire");
        stay = true;
        yield return new WaitForSeconds(0.5f);
        GameObject bull = PoolControll.Instance.Spawn_enemy_bullet();
        bull.transform.position = new Vector3(transform.position.x, transform.position.y + bullet_start_pos , transform.position.z);
        bull.transform.rotation = fire_position.transform.rotation;
        yield return new WaitForSeconds(1);
        fire_timer = Random.Range(2f, 4f);       
        stay = false;
        anim.SetTrigger("move");
    }
    public void Dead()
    {
        if(Player_controll.Instance.jump)
        {
            GameObject obj = Instantiate(emojy[Random.Range(0, emojy.Length)]) as GameObject;
            obj.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z + 5);
            Destroy(obj, 2);
        }
        dead = true;
        anim.SetTrigger("dead");
        GetComponent<CapsuleCollider>().enabled = false;        
    }
    
    //private void OnBecameInvisible()
    //{
    //    gameObject.SetActive(false);
    //}
    //private void OnTriggerEnter(Collider coll)
    //{
    //    if (coll.gameObject.tag == "Player")
    //    { 
    //        GetComponent<Enemy>().Damage(10);           
    //    }
    //}
}
