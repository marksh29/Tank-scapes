using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Game_Controll : MonoBehaviour
{
    public static Game_Controll Instance;
    public bool game, pause;
    [SerializeField] GameObject lose_panel;
    [SerializeField] Text money_text;
    [SerializeField] int money_int;
    
    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        money_int = 0;
        money_text.text = money_int.ToString();
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
        Time.timeScale = 1;
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene("game 1");
    }
    public void Lose()
    {
        lose_panel.SetActive(true);
        Player_controll.Instance.game = false;
    }
}
