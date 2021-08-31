using UnityEngine;
using System.Collections;

public class Terra : MonoBehaviour 
{
    int cur_terr_id;
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
        GameObject obj = Instantiate(TerrPrefab[cur_terr_id], new Vector3(curTerr.transform.position.x,curTerr.transform.position.y, curTerr.transform.position.z + dlinaTerra), curTerr.transform.rotation) as GameObject;
        curTerr = obj;
        stroyLevel = true;
        cur_terr_id = cur_terr_id + 1 == TerrPrefab.Length ? 0 : cur_terr_id + 1;
    }
}
