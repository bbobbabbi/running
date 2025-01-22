using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction attackAction;
    public PlayerController controller;

    void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        var playercController = controller;

        playercController.OnTrrigger("Jump", attackAction.triggered);


    }
}
