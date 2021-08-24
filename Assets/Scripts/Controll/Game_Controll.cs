using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Game_Controll : MonoBehaviour
{
    public Canvas canvas;
    public static Game_Controll Instance;
    public bool game, pause;

    [SerializeField]
    GameObject lose_panel;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        if (Instance == null)
            Instance = this;
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
            canvas.sortingOrder = 0;
            Time.timeScale = 1;
        }
        else
        {
            pause = true;
            canvas.sortingOrder = 111;
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
