using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controll : MonoBehaviour
{
    [SerializeField] private Player_stats stats;
    public static Player_controll Instance;
    [SerializeField] private GameObject _camera;   
    [SerializeField] private int pos_state;
    [SerializeField] private float move_speed, rotate_speed, jump_speed, down_speed, fire_speed;
    public bool game, move, jump, down;
    [SerializeField] private List<GameObject> enemys;
    [SerializeField] Transform fire_pos;
    [SerializeField] private float[] xx_pos;
    [SerializeField] private Animator player_anim, up_anim, fire_anim, down_anim;
    [SerializeField] private Slider energy;

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

        
        down_anim.speed = Player_stats.Instance.down_speed;

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

            if (duble_clik_time > 0)
                duble_clik_time -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && !jump)
            {
                if (energy.value > 0.2f && fire_speed <= 0 && !down)
                {
                    StartCoroutine(Fire());
                    fire_speed = Player_stats.Instance.attack_speed;
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

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip up
                {
                    player_anim.speed = Player_stats.Instance.jump_speed;
                    jump = true;
                    player_anim.SetTrigger("up");
                    StartCoroutine(Off(1/player_anim.speed));
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

    private void LateUpdate()
    {
        for(int i = 0; i < enemys.Count; i++)
        {

        }
    }
    IEnumerator Fire()
    {
        fire_pos.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        fire_pos.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Add_enemy(GameObject obj)
    {
        enemys.Add(obj);
    }
    public void Remove_enemy(GameObject obj)
    {
        enemys.Remove(obj);
    }    
}
