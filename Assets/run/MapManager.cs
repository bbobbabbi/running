using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static Pool pool;
    GameObject currentMap;
    GameObject nextMap;
    bool isNextMapPlaced = true;
    bool isCurrentMapReturned = false;
    // Start is called before the first frame update
    void Start()
    {
        pool = Pool.Instance;
        currentMap = GameObject.Find("Ground");
    }
    private void Update()
    {
        if ( currentMap.transform.position.z < -3.25f)
        {
            _ = StartCoroutine(GetMap());
            pool.ReturnMap(currentMap);
            currentMap = nextMap;
        }
    }
    IEnumerator GetMap() { 
        nextMap= pool.GetMap();
        nextMap.transform.position = new Vector3(0,0, 16.5f);        
        yield return null;
    }

}
