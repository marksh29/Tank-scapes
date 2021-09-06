using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour
{
    [SerializeField] GameObject[] tank;
    [SerializeField] GameObject _cam,_pos;
    [SerializeField] float speed;
    void Start()
    {
        StartCoroutine(T());
    }
    IEnumerator T()
    {
        yield return new WaitForSeconds(2);
        tank[0].SetActive(false);
        tank[1].SetActive(true);
        yield return new WaitForSeconds(2);
        tank[1].SetActive(false);
        tank[2].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        _cam.transform.RotateAround(_pos.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
