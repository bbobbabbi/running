using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody playerrb;
    // Start is called before the first frame update
    void Start()
    {
        playerrb = GetComponent<Rigidbody>();
        playerrb.AddForce(Vector3.up*1000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
