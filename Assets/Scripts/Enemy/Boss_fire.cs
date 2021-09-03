using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_fire : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject player, fire_position;
    [SerializeField] int cur_pos;
    [SerializeField] float[] min_max_pos;
    [SerializeField] float speed, move_timer, bullet_start_pos;
    public bool stay, dead, fire, end;
    [SerializeField] GameObject[] emojy;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");       
    }
    void Start()
    {
        Change_move();
    }
    void Update()
    {
        if (!dead)
        {          
            Vector3 targetDirection = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5) - gameObject.transform.position;
            float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(gameObject.transform.forward, targetDirection, singleStep, 0.0f);
            gameObject.transform.rotation = Quaternion.LookRotation(newDirection);         
        }
    }   
    void Change_move()
    {
        print("Change_move");
        anim.SetTrigger("move");
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
        StartCoroutine(Fire_on(Random.Range(5, 10)));
        anim.SetTrigger("fire");
    }
    IEnumerator Fire_on(int fire_count)
    {        
        for(int i = 0; i < fire_count; i++)
        {
            GameObject bull = PoolControll.Instance.Spawn_enemy_bullet();
            bull.transform.position = new Vector3(transform.position.x, transform.position.y + bullet_start_pos, transform.position.z);
            bull.transform.rotation = fire_position.transform.rotation;
            yield return new WaitForSeconds(0.6f);
        }        
        Change_move();        
    }
    public void Dead()
    {
        StopAllCoroutines();        
        dead = true;
        anim.SetTrigger("dead");
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
