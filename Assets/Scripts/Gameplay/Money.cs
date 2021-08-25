using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] float speed;
    void Start()
    {
        speed = Random.Range(50f, 200f);
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Game_Controll.Instance.Add_money(1);
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
