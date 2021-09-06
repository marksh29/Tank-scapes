using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] float speed, force;
    void Start()
    {
        Drop();
        speed = Random.Range(50f, 200f);
    }
    private void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        if (coll.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            Game_Controll.Instance.Add_money(1);
            Destroy(gameObject, 2);
        }
    }
   public void Drop()
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.2f,0.2f), 0.6f, Random.Range(-0.2f, 0.2f)) * force * Time.deltaTime, ForceMode.Impulse);
    }
}
