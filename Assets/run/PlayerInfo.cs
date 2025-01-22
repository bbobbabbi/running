using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public Vector3 firstPosition;
    public float gas = 100;
    private void Awake()
    {
        firstPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("pizza")) {
            gas += 30;
            Destroy(other.gameObject);
        }
    }
}
