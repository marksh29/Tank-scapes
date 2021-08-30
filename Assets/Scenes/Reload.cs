using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("1");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
