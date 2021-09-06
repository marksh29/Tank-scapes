using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controll : MonoBehaviour
{
    public static Player_controll Instance;
    [SerializeField] private GameObject _camera, cur_enemy;   
    [SerializeField] private int pos_state;
    [SerializeField] private float fire_speed, speed, max_speed, jump_target_x;
    public bool game, move, jump, up_jump, down, swipe_controll, enemy_attack, back;    
    [SerializeField] Transform[] fire_pos;
    [SerializeField] private float[] xx_pos;
    [SerializeField] private Animator player_anim, up_anim, down_anim;
    [SerializeField] private Slider energy;
    [SerializeField] private GameObject[] enemys, flame, smoke;
    [SerializeField] bool boss_fire;
    

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    float duble_clik_time;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        max_speed = Player_stats.Instance.move_speed;

        swipe_controll = Player_stats.Instance.swipe_controll;
        down_anim.gameObject.GetComponent<Animator>().enabled = !swipe_controll; 

        fire_speed = Player_stats.Instance.attack_speed;
        energy.value = 1;
        pos_state = 2;

        energy.maxValue = Player_stats.Instance.attack_speed;
        game = true;
    }
    void Update()
    {
        if (game)
        {
            //_camera.transform.position = new Vector3(player.transform.position.x, _camera.transform.position.y, transform.position.z);

            if (fire_speed > 0)
                fire_speed -= Time.deltaTime;
            energy.value = Player_stats.Instance.attack_speed - fire_speed;

            if (speed != max_speed)
                speed += (speed < max_speed ? Player_stats.Instance.move_remove : (-Player_stats.Instance.move_remove * 3)) * Time.deltaTime;
        
            if(!boss_fire)
                transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (duble_clik_time > 0)  // -- double click timer remove
                duble_clik_time -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && !jump && !up_jump && !back)
            {
                if (Player_stats.Instance.auto_fire)  // --- Прицеливание по клику
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit);
                    if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
                    {
                        cur_enemy = hit.collider.gameObject;
                    }
                }               

                if (!move)
                {
                    if (duble_clik_time > 0)
                    {
                        if (Mathf.Abs(Camera.main.WorldToScreenPoint(transform.position).y - Input.mousePosition.y) > Mathf.Abs(Camera.main.WorldToScreenPoint(transform.position).x - Input.mousePosition.x))
                        {
                            Up_jump();
                        }
                        else
                        {
                            Jump(Input.mousePosition.x - Camera.main.WorldToScreenPoint(transform.position).x > 0 ? 1 : 0);
                        }                        
                    }
                    else
                        duble_clik_time = 0.3f;
                }
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
           
            if(Input.GetMouseButton(0) && swipe_controll && !jump && !up_jump && !back)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (Vector3.Magnitude(secondPressPos - firstPressPos) > 50)
                {
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && transform.position.x > xx_pos[0])//transform.position.x > xx_pos[0] && !jump) // swip left
                    {
                        move = true;
                        down_anim.gameObject.transform.rotation = Quaternion.Euler(0, -20, 0);
                        transform.Translate(Vector2.left * (Player_stats.Instance.swipe_speed * Speed_count()) * Time.deltaTime);                      
                    }                    
                    else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && transform.position.x < xx_pos[4])//transform.position.x < xx_pos[4] && !jump) // swip right
                    {
                        move = true;
                        down_anim.gameObject.transform.rotation = Quaternion.Euler(0, 20, 0);
                        transform.Translate(Vector2.right * (Player_stats.Instance.swipe_speed * Speed_count()) * Time.deltaTime);                        
                    }
                    else
                    {
                        down_anim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }                
            }

            if(Input.GetMouseButtonUp(0))
            {
                if (swipe_controll && !jump)
                {
                    move = false;
                    down_anim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }               
            }           
        }
    }
    float Speed_count()
    {
        float sp = new float();
        if(cur_enemy == null || cur_enemy != null && !cur_enemy.GetComponent<Enemy>().boss_fire)
        {
            sp = max_speed == Player_stats.Instance.move_speed ? (speed / max_speed) : (max_speed / speed);
        }
        else if(cur_enemy != null && cur_enemy.GetComponent<Enemy>().boss_fire)
        {
            sp = 1;
        }
        return sp;
    }
  
    private void LateUpdate()
    {
        if (cur_enemy == null)
        {
            enemys = new GameObject[0];
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemys.Length; i++)
            {
                if ((enemys[i].transform.position.z > transform.position.z + 10) && (cur_enemy == null || cur_enemy != null && enemys[i].transform.position.z < cur_enemy.transform.position.z))
                {
                    cur_enemy = enemys[i];
                }
                //float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
                //Vector3 newDirection = Vector3.RotateTowards(up_anim.gameObject.transform.forward, transform.position, singleStep, 0.0f);
                //up_anim.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
            }
            max_speed = Player_stats.Instance.move_speed;
        }

        if (cur_enemy != null && !jump && !up_jump)
        {            
            if (cur_enemy.transform.position.z - transform.position.z < Player_stats.Instance.attack_distance)
            {
                if(cur_enemy.GetComponent<Enemy>().boss_fire)
                    boss_fire = true;
               
                if (Player_stats.Instance.auto_fire)  // --- auto shoot
                {
                    Vector3 targetDirection = new Vector3(cur_enemy.transform.position.x, up_anim.gameObject.transform.position.y, cur_enemy.transform.position.z) - up_anim.gameObject.transform.position;
                    float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
                    Vector3 newDirection = Vector3.RotateTowards(up_anim.gameObject.transform.forward, targetDirection, singleStep, 0.0f);
                    up_anim.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
                   
                    RaycastHit hit;
                    Physics.Raycast(fire_pos[Player_ugrade.Instance.state_id].position, fire_pos[Player_ugrade.Instance.state_id].TransformDirection(Vector3.forward), out hit, Player_stats.Instance.enemy_distance);
                    if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")// && hit.collider.gameObject == cur_enemy)
                    {
                        if (fire_speed <= 0 && !down) // --- auto shoot timer
                        {
                            fire_speed = Player_stats.Instance.attack_speed;
                            StartCoroutine(Fire());
                        }
                    }
                }
                max_speed = Player_stats.Instance.frize_move_speed;
               
                if (fire_speed <= 0 && !down && !Player_stats.Instance.auto_fire) // --- auto shoot timer 
                {
                    fire_speed = Player_stats.Instance.attack_speed;
                    StartCoroutine(Fire());
                }              
            }
        }       
    }
    void Up_jump()
    {
        up_jump = true;
        for (int i = 0; i < flame.Length; i++)
        {
            flame[i].SetActive(true);
        }
        player_anim.speed = Player_stats.Instance.jump_speed;       
        player_anim.SetTrigger("up");
        StartCoroutine(Off(1 / player_anim.speed));
    }
    void Jump(int id)
    {
        if (id == 1)  // ---вправо
        {
            jump = true;
            player_anim.speed = Player_stats.Instance.duble_jump_speed;
            player_anim.SetTrigger("right");
            StartCoroutine(Off(1/Player_stats.Instance.duble_jump_speed));
            for (int i = 0; i < 2; i++)
            {
                flame[i].SetActive(true);
            }

            if (transform.position.x + Player_stats.Instance.duble_jump_dist < xx_pos[4])
                StartCoroutine(DoMove(Player_stats.Instance.duble_jump_speed, transform.position.x + Player_stats.Instance.duble_jump_dist));
            else
                StartCoroutine(DoMove(Player_stats.Instance.duble_jump_speed, xx_pos[xx_pos.Length - 1]));                  
        }       
        if (id == 0) //--- влево
        {
            jump = true;
            player_anim.speed = Player_stats.Instance.duble_jump_speed;
            player_anim.SetTrigger("left");
            StartCoroutine(Off(1/Player_stats.Instance.duble_jump_speed));
            for (int i = 2; i < flame.Length; i++)
            {
                flame[i].SetActive(true);
            }

            if (transform.position.x - Player_stats.Instance.duble_jump_dist > xx_pos[0])
                StartCoroutine(DoMove(Player_stats.Instance.duble_jump_speed, transform.position.x - Player_stats.Instance.duble_jump_dist));
            else
                StartCoroutine(DoMove(Player_stats.Instance.duble_jump_speed, xx_pos[0]));           
        }       
        duble_clik_time = 0f;
    }
    private IEnumerator DoMove(float time, float xx)
    {
        Vector2 startPosition = transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            transform.position = Vector3.Lerp(transform.position, new Vector3(xx, transform.position.y, transform.position.z), fraction);
            yield return null;
        }
        print("end");
    }
  
    private IEnumerator Off(float time)
    {
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].SetActive(false);
        }
        yield return new WaitForSeconds(time);
        up_jump = false;
        jump = false;
        down = false;
        move = false;
        StopAllCoroutines();
        for (int i = 0; i < flame.Length; i++)
        {
            flame[i].SetActive(false);
        }
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].SetActive(true);
        }       
    }
       
    public void Cleare_enemy()
    {
        cur_enemy = null;
        boss_fire = false;
    }

    IEnumerator Fire()
    {
        fire_pos[Player_ugrade.Instance.state_id].transform.GetChild(0).gameObject.SetActive(true);
        GameObject bull = (Player_ugrade.Instance.state_id == 0 ?  PoolControll.Instance.Spawn_player_bullet_1() : PoolControll.Instance.Spawn_player_bullet_2());
        bull.transform.position = fire_pos[Player_ugrade.Instance.state_id].transform.GetChild(0).position;
        bull.transform.rotation = fire_pos[Player_ugrade.Instance.state_id].transform.rotation;       
        yield return new WaitForSeconds(0.2f);
        if (Player_ugrade.Instance.state_id == 1)
        {
            fire_pos[Player_ugrade.Instance.state_id].transform.GetChild(1).gameObject.SetActive(true);
            GameObject bull1 = PoolControll.Instance.Spawn_player_bullet_2();
            bull1.transform.position = fire_pos[Player_ugrade.Instance.state_id].transform.GetChild(1).position;
            bull1.transform.rotation = fire_pos[Player_ugrade.Instance.state_id].transform.rotation;
        }
        yield return new WaitForSeconds(0.3f);
        fire_pos[Player_ugrade.Instance.state_id].transform.GetChild(0).gameObject.SetActive(false);
        if (Player_ugrade.Instance.state_id == 1)
        {
            fire_pos[Player_ugrade.Instance.state_id].transform.GetChild(1).gameObject.SetActive(false);
        }
    }  
    public void Block()
    {
        back = true;
        Player_controll.Instance.speed = 10;
        transform.position = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
        StartCoroutine(BlockMove(1, transform.position.z - 15));
    }
    private IEnumerator BlockMove(float time, float zz)
    {
        Vector2 startPosition = transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, zz), fraction);
            yield return null;
        }
        back = false;
    }
}
