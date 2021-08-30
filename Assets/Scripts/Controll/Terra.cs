using UnityEngine;
using System.Collections;

public class Terra : MonoBehaviour 
{
    public GameObject[] TerrPrefab;
    public Transform Player;
    public GameObject curTerr;
    public float playerPoz;
	public float dlinaTerra, rasst_ot_nachala; //вычислена экспериментально :)
    public bool stroyLevel = true;

    // Update is called once per frame
    void Update()
    {      
        if (Player.position.z + rasst_ot_nachala >= curTerr.transform.position.z)
        {
            stroyLevel = false;
            CreateSegment();
        }
    }

    void CreateSegment()
    {
        GameObject obj = Instantiate(TerrPrefab[Random.Range(0, TerrPrefab.Length)], new Vector3(curTerr.transform.position.x,curTerr.transform.position.y, curTerr.transform.position.z + dlinaTerra), curTerr.transform.rotation) as GameObject;
        Destroy(curTerr, 15);
        curTerr = obj;
        stroyLevel = true;
    }
}
