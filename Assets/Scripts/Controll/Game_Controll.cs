using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VacuumShaders.CurvedWorld;

public class Game_Controll : MonoBehaviour
{

    public static Game_Controll Instance;
    public bool game, pause;
    [SerializeField] GameObject lose_panel;
    [SerializeField] Text money_text;
    [SerializeField] int money_int;
    [SerializeField] float curve_timer, curve_xx, curve_speed;
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        game = true;
        money_int = 0;
        money_text.text = money_int.ToString();
        curve_timer = Random.Range(5, 10); 
        
    }
    private void Update()
    {
        //if(game)
        //{
        //    curve_timer -= Time.deltaTime;
        //    if(curve_timer <= 0)
        //    {
        //        Change_crve();
        //    }

        //    if(CurvedWorld_Controller.current.leftRightSize != curve_xx)
        //    {
        //        if((curve_xx > 0 && CurvedWorld_Controller.current.leftRightSize < curve_xx) || (curve_xx < 0 && CurvedWorld_Controller.current.leftRightSize > curve_xx) || (curve_xx == 0 && (CurvedWorld_Controller.current.leftRightSize > -0.01f && CurvedWorld_Controller.current.leftRightSize < 0.01)))
        //            CurvedWorld_Controller.current.leftRightSize += (CurvedWorld_Controller.current.leftRightSize < curve_xx ? curve_speed : -curve_speed) * Time.deltaTime;
        //    }
        //}
    }
    void Change_crve()
    {
        if (curve_xx == 0)
        {
            curve_xx = Random.Range(-1f, 1f);
            curve_timer = Random.Range(5f, 10f);
        }
        else
        {
            curve_xx = 0;
            curve_timer = Random.Range(10f, 19f);
        }
    }

    public void Add_money(int id)
    {
        money_int += id;
        money_text.text = money_int.ToString();
    }

    public void Load_level(string name)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(name);
    }

    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            pause = false;
            Time.timeScale = 1;
        }
        else
        {
            pause = true;
            Time.timeScale = 0;
        }
    }
    public void Click()
    {
        if (Sound.Instance != null)
            Sound.Instance.Click();
    }

    public void Continue()
    {
        game = true;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene("game 1");
    }
    public void Lose()
    {
        game = false;
        lose_panel.SetActive(true);
        Player_controll.Instance.game = false;
    }
}
