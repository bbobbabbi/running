using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool onTriggerTrigger;

    public void OnTrrigger(string action, bool isTrigger)
    {
        Jump(action, isTrigger);
    }
    private void Jump(string action, bool isTrigger)
    {
        if (action == "Jump" && isTrigger && !onTriggerTrigger)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10,ForceMode.Impulse);
            onTriggerTrigger = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            onTriggerTrigger = false;
            Debug.Log("¶¥±îÁö ´êÀ½");
        }
        Debug.Log("¿ÂÄÝ¸®Àü");

    }

}

