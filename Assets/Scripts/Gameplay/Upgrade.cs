using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool up;
    void Start()
    {
        speed = Random.Range(50f, 200f);
    }
    private void Update()
    {
        if(up)
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        else
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            Player_ugrade.Instance.Update_tank(1);
            Destroy(gameObject, 2);
        }
    }
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
    }
}
