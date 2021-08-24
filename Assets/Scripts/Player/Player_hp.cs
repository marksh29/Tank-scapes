using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_hp : MonoBehaviour
{
    public static Player_hp Instance;
    [SerializeField] Slider life;
    [SerializeField] GameObject blood;
    [SerializeField]
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        life.value = 1;        
    }
    void Update()
    {
                 
    }   
    public void Heal()
    {
        life.value += 0.1f;
    }
    public void Damage(float count)
    {
        life.value -= count;
        StartCoroutine(Blood_on());

        if(life.value <= 0)
        {
            Game_Controll.Instance.Lose();
        }
    }    
    IEnumerator Blood_on()
    {
        blood.SetActive(true);
        yield return new WaitForSeconds(1);
        blood.SetActive(false);
    }
}