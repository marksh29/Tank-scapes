using UnityEngine;
using System.Collections;

public class Terra : MonoBehaviour 
{
    public GameObject[] TerrPrefab;
    public Transform Player;
    public GameObject curTerr, old_terra;
    public float playerPoz;
	public float dlinaTerra; //вычислена экспериментально :)
    public bool stroyLevel = true;

    // Update is called once per frame
    void Update()
    {
        playerPoz = Player.position.z;
      
        if (playerPoz >= curTerr.transform.position.z + 50)
        {
            stroyLevel = false;
            CreateSegment(old_terra);
        }
    }

    void CreateSegment(GameObject terr)
    {
        old_terra.transform.position = new Vector3(curTerr.transform.position.x, curTerr.transform.position.y, curTerr.transform.position.z + dlinaTerra);
        old_terra = curTerr;
        curTerr = terr;
        stroyLevel = true;
    }
}
