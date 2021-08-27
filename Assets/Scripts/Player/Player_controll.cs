using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controll : MonoBehaviour
{
    public static Player_controll Instance;
    [SerializeField] private GameObject _camera, cur_enemy;   
    [SerializeField] private int pos_state;
    [SerializeField] private float move_speed, fire_speed;
    public bool game, move, jump, down;    
    [SerializeField] Transform[] fire_pos;
    [SerializeField] private float[] xx_pos;
    [SerializeField] private Animator player_anim, up_anim, down_anim;
    [SerializeField] private Slider energy;

    [SerializeField] private GameObject[] enemys;

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
        move_speed = Player_stats.Instance.move_speed;
        fire_speed = Player_stats.Instance.attack_speed;
        energy.value = 1;
        pos_state = 2;
        transform.position = new Vector2(xx_pos[pos_state], 0);
        game = true;
    }
    void Update()
    {
        if(game)
        {
            if (energy.value < 1)
                energy.value += 0.1f * Time.deltaTime;
            if(fire_speed > 0)
                fire_speed -= Time.deltaTime;

            transform.Translate(Vector3.forward * move_speed * Time.deltaTime);

            _camera.transform.position = new Vector3(transform.position.x, _camera.transform.position.y, transform.position.z);

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

            if (Input.GetMouseButtonUp(0) && !move && !jump && energy.value >= 0.2f)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (Vector3.Magnitude(secondPressPos - firstPressPos) > 100)
                {
                    if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip up
                    {
                        player_anim.speed = Player_stats.Instance.jump_speed;
                        jump = true;
                        player_anim.SetTrigger("up");
                        StartCoroutine(Off(1 / player_anim.speed));
                    }
                    if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip down
                    {
                        up_anim.speed = Player_stats.Instance.down_speed;
                        down = true;
                        up_anim.SetTrigger("down");
                        StartCoroutine(Off(1 / up_anim.speed));
                    }
                    if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && pos_state > 0) // swip left
                    {
                        down_anim.speed = Player_stats.Instance.rotate_speed;
                        pos_state = pos_state - 1;
                        StartCoroutine(DoMove(1 / down_anim.speed));
                        move = true;
                        down_anim.SetTrigger("left");
                        StartCoroutine(Off(1 / down_anim.speed));
                    }
                    if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && pos_state < 4) // swip right
                    {
                        down_anim.speed = Player_stats.Instance.rotate_speed;
                        pos_state = pos_state + 1;
                        StartCoroutine(DoMove(1 / down_anim.speed));
                        move = true;
                        down_anim.SetTrigger("right");
                        StartCoroutine(Off(1 / down_anim.speed));
                    }
                    energy.value -= 0.2f;
                }
            }
        }
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
            }
        }

        if (cur_enemy != null && !jump)
        {
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
    }
    void Jump(int id)
    {
        if (pos_state < 3 && id == 1)
        {
            player_anim.speed = Player_stats.Instance.duble_jump_speed;
            pos_state = pos_state + 2;
            StartCoroutine(DoMove(1/player_anim.speed));
            jump = true;
            player_anim.SetTrigger("right");
            StartCoroutine(Off(1 / player_anim.speed));
        }
        if (pos_state > 1 && id == 0)
        {
            player_anim.speed = Player_stats.Instance.duble_jump_speed;
            pos_state = pos_state - 2;
            StartCoroutine(DoMove(1 / player_anim.speed));
            jump = true;
            player_anim.SetTrigger("left");
            StartCoroutine(Off(1 / player_anim.speed));
        }
        duble_clik_time = 0f;
    } 
    
    private IEnumerator DoMove(float time)
    {
        Vector2 startPosition = transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;
        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time);
            transform.position = Vector3.Lerp(new Vector3(startPosition.x, transform.position.y, transform.position.z), new Vector3(xx_pos[pos_state], transform.position.y, transform.position.z), fraction);
            yield return null;
        }       
    }
    private IEnumerator Off(float time)
    {
        yield return new WaitForSeconds(time);
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
        GameObject bull = PoolControll.Instance.Spawn_player_bullet();
        bull.transform.GetChild(0).gameObject.SetActive(Player_ugrade.Instance.state_id == 0 ? false : true); 
        bull.transform.position = fire_pos[Player_ugrade.Instance.state_id].transform.position;
        bull.transform.rotation = fire_pos[Player_ugrade.Instance.state_id].transform.rotation;
        yield return new WaitForSeconds(0.3f);
        fire_pos[Player_ugrade.Instance.state_id].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider coll)
    {
        
    }
}
