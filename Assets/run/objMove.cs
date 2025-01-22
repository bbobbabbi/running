using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapMove : MonoBehaviour
{
   
    private Pool pool;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (pool == null) pool = Pool.Instance;
        if (gm == null) gm = GameManager.Instance;
        if(transform.position.z < -25)
            Destroy(gameObject);
        if (pool.isPoolReady)
            transform.position += -Vector3.forward * Time.deltaTime * gm.speed;
    }
}
