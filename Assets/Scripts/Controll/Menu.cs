using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class Menu : MonoBehaviour
    {
        private void Awake()
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        private void Update()
        {
           
        }
        private void Start()
        {
        }       
        public void Play()
        {
            SceneManager.LoadScene("Game");      
        }        
        public void Click()
        {
            Sound.Instance.Click();
        }       
        public void Quit()
        {
            Application.Quit();
        }        
    }
}
