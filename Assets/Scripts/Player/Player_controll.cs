using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_controll : MonoBehaviour
{
    public static Player_controll Instance;
    [SerializeField] private GameObject _camera;   
    [SerializeField] private int pos_state;
    [SerializeField] private float speed;
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
            {
                energy.value += 0.1f * Time.deltaTime;
            }


            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            _camera.transform.position = new Vector3(0, transform.position.y, transform.position.z);

            if (duble_clik_time > 0)
                duble_clik_time -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && !jump && !move)
            {
                if (duble_clik_time > 0)
                {
                    Jump(Camera.main.ScreenPointToRay(Input.mousePosition).direction.x > 0 ? 1 : 0);
                }                    
                else
                    duble_clik_time = 0.3f;

                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButtonUp(0) && !move && !jump && energy.value >= 0.2f)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip up
                {
                    jump = true;
                    player_anim.SetTrigger("up");
                    StartCoroutine(Off(1));
                }
                if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) // swip down
                {
                    down = true;
                    up_anim.SetTrigger("down");
                    StartCoroutine(Off(1));
                }
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && pos_state > 0) // swip left
                {
                    if (pos_state > 0)
                        pos_state = pos_state - 1;                   
                    StartCoroutine(DoMove(1));
                    move = true;
                    down_anim.SetTrigger("left");
                    StartCoroutine(Off(1));
                }
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f && pos_state < 4) // swip right
                {
                    if (pos_state < 4)
                        pos_state = pos_state + 1;
                    StartCoroutine(DoMove(1));
                    move = true;
                    down_anim.SetTrigger("right");
                    StartCoroutine(Off(1));
                }

                energy.value -= 0.2f;
            }
        }
    }
    void Jump(int id)
    {
        if (pos_state < 3 && id == 1)
        {
            pos_state = pos_state + 2;
            StartCoroutine(DoMove(1));
            jump = true;
            player_anim.SetTrigger("right");
            StartCoroutine(Off(1));
        }
        if (pos_state > 1 && id == 0)
        {
            pos_state = pos_state - 2;
            StartCoroutine(DoMove(1));
            jump = true;
            player_anim.SetTrigger("left");
            StartCoroutine(Off(1));
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
    void Fire()
    {
        if(!jump && !move && !down)
        {

        }
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
