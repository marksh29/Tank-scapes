using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] Material[] mat;
    void Start()
    {
        
    }
    public void Set_color(int id)
    {
        GetComponent<MeshRenderer>().material = mat[id];
    }   
}
