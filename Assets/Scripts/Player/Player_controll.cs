using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controll : MonoBehaviour
{
    public static Player_controll Instance;
    [SerializeField] private GameObject _camera, cur_enemy;   
    [SerializeField] private int pos_state;
    [SerializeField] private float fire_speed, speed, max_speed;
    public bool game, move, jump, down, swipe_controll, enemy_attack;    
    [SerializeField] Transform[] fire_pos;
    [SerializeField] private float[] xx_pos;
    [SerializeField] private Animator player_anim, up_anim, down_anim;
    [SerializeField] private Slider energy;

    [SerializeField] private GameObject[] enemys, flame, smoke;

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
        transform.position = new Vector3(xx_pos[pos_state], transform.position.y, transform.position.z);
        game = true;
    }
    void Update()
    {
        if (game)
        {
            //if (energy.value < 1)
            //    energy.value += 0.1f * Time.deltaTime;

            if(fire_speed > 0)
                fire_speed -= Time.deltaTime;

            if (speed != max_speed)
                speed += (speed < max_speed ? Player_stats.Instance.move_remove : (-Player_stats.Instance.move_remove * 3)) * Time.deltaTime;

            transform.Translate(Vector3.forward * speed * Time.deltaTime);

           // _camera.transform.position = new Vector3(transform.position.x, _camera.transform.position.y, transform.position.z);

            if (duble_clik_time > 0)  // -- double click timer remove
                duble_clik_time -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && !jump)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);
                if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
                {
                    cur_enemy = hit.collider.gameObject;
                }               

                if (!move)
                {
                    if (duble_clik_time > 0)
                    {
                        Jump(Camera.main.ScreenPointToRay(Input.mousePosition).direction.x > 0 ? 1 : 0);
                    }
                    else
                        duble_clik_time = 0.3f;
                }
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
           
            if(Input.GetMouseButton(0) && swipe_controll)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (Vector3.Magnitude(secondPressPos - firstPressPos) > 100)
                {
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && !jump && !move) // swip up
                    {
                        for (int i = 0; i < flame.Length; i++)
                        {
                            flame[i].SetActive(true);
                        }

                        player_anim.speed = Player_stats.Instance.jump_speed;
                        jump = true;
                        player_anim.SetTrigger("up"); 
                        StartCoroutine(Off(1 / player_anim.speed));
                    }
                    if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f && !down) // swip down
                    {
                        up_anim.speed = Player_stats.Instance.down_speed;
                        down = true;
                        up_anim.SetTrigger("down");
                        StartCoroutine(Off(1 / up_anim.speed));
                    }

                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)//transform.position.x > xx_pos[0] && !jump) // swip left
                    {
                        move = true;
                        down_anim.gameObject.transform.rotation = Quaternion.Euler(0, -20, 0);
                        transform.Translate(Vector2.left * (Player_stats.Instance.swipe_speed * Speed_count()) * Time.deltaTime);
                        if (transform.position.x <= xx_pos[0])
                            down_anim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)//transform.position.x < xx_pos[4] && !jump) // swip right
                    {
                        move = true;
                        down_anim.gameObject.transform.rotation = Quaternion.Euler(0, 20, 0);
                        transform.Translate(Vector2.right * (Player_stats.Instance.swipe_speed * Speed_count()) * Time.deltaTime);
                        if (transform.position.x >= xx_pos[4])
                            down_anim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    energy.value -= 0.02f;
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                if (swipe_controll && !jump)
                {
                    move = false;
                    down_anim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                if (!move && !jump && energy.value >= 0.2f)
                {
                    secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                    currentSwipe.Normalize();

                    if (Vector3.Magnitude(secondPressPos - firstPressPos) > 100)
                    {
                        if (!swipe_controll)
                        {
                            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && pos_state > 0) // swip left
                            {
                                down_anim.speed = Player_stats.Instance.rotate_speed;
                                pos_state = pos_state - 1;
                                StartCoroutine(DoMove(1 / down_anim.speed, xx_pos[pos_state]));
                                move = true;
                                down_anim.SetTrigger("left");
                                StartCoroutine(Off(1 / down_anim.speed));
                            }
                            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && pos_state < 4) // swip right
                            {
                                down_anim.speed = Player_stats.Instance.rotate_speed;
                                pos_state = pos_state + 1;
                                StartCoroutine(DoMove(1 / down_anim.speed, xx_pos[pos_state]));
                                move = true;
                                down_anim.SetTrigger("right");
                                StartCoroutine(Off(1 / down_anim.speed));
                            }
                            energy.value -= 0.2f;
                        }                       
                    }
                }
            }           
        }
    }
    float Speed_count()
    {
        float sp = new float();
        sp = max_speed == Player_stats.Instance.move_speed ? (speed / max_speed) : (max_speed/ speed);
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
                float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(up_anim.gameObject.transform.forward, transform.position, singleStep, 0.0f);
                up_anim.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
        if (cur_enemy != null && !jump)
        {
            if(cur_enemy.transform.position.z - transform.position.z < Player_stats.Instance.enemy_distance - 20)
            {
                enemy_attack = true;
                max_speed = Player_stats.Instance.frize_move_speed;

                Vector3 targetDirection = cur_enemy.transform.position - up_anim.gameObject.transform.position;
                float singleStep = Player_stats.Instance.up_speed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(up_anim.gameObject.transform.forward, targetDirection, singleStep, 0.0f);
                up_anim.gameObject.transform.rotation = Quaternion.LookRotation(newDirection);

                if (cur_enemy.transform.position.z < transform.position.z + 10)
                    cur_enemy = null;

                RaycastHit hit;
                Physics.Raycast(fire_pos[Player_ugrade.Instance.state_id].position, fire_pos[Player_ugrade.Instance.state_id].TransformDirection(Vector3.forward), out hit, 9990);
                if (hit.collider != null && hit.collider.gameObject.tag == "Enemy" && hit.collider.gameObject == cur_enemy)
                {
                    if (fire_speed <= 0 && !down) // --- auto shoot timer
                    {
                        fire_speed = Player_stats.Instance.attack_speed;
                        StartCoroutine(Fire());
                    }
                }
            }
            else
            {
                enemy_attack = false;
                max_speed = Player_stats.Instance.move_speed;
            }
        }
    }
    void Jump(int id)
    {
        if (id == 1 && (transform.position.x + 7 < 10))  // ---в право
        {
            for (int i = 0; i < 2; i++)
            {
                flame[i].SetActive(true);
            }
            player_anim.speed = Player_stats.Instance.duble_jump_speed;
            pos_state = pos_state + 2;
            StartCoroutine(DoMove(1/player_anim.speed, transform.position.x + 7));
            jump = true;
            player_anim.SetTrigger("right");
            StartCoroutine(Off(1 / player_anim.speed));
        }
        if (id == 0 && (transform.position.x - 7 > -14)) //--- в лево
        {
            for (int i = 2; i < flame.Length; i++)
            {
                flame[i].SetActive(true);
            }
            player_anim.speed = Player_stats.Instance.duble_jump_speed;
            pos_state = pos_state - 2;
            StartCoroutine(DoMove(1 / player_anim.speed, transform.position.x - 7));
            jump = true;
            player_anim.SetTrigger("left");
            StartCoroutine(Off(1 / player_anim.speed));
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
            transform.position = Vector3.Lerp(new Vector3(startPosition.x, transform.position.y, transform.position.z), new Vector3(xx, transform.position.y, transform.position.z), fraction);
            yield return null;
        }       
    }
    private IEnumerator Off(float time)
    {
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].SetActive(false);
        }
        yield return new WaitForSeconds(time);
        for(int i =0; i < flame.Length; i++)
        {
            flame[i].SetActive(false);
        }
        for (int i = 0; i < smoke.Length; i++)
        {
            smoke[i].SetActive(true);
        }
        jump = false;
        down = false;
        move = false;
    }

   
    public void Cleare_enemy(GameObject obj)
    {
        cur_enemy = null;
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

    public void OnTriggerEnter(Collider coll)
    {
        
    }
    public void Full_stop()
    {
        game = false;
    }
}
