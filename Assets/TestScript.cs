using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestScript : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerInput playerInput;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;
        //playerInputActions.Player.Movement.performed += Movement_Performed;
    }
    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        if (playerInputActions.Player.Jump.IsPressed()) rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        Debug.Log(inputVector);
        float speed = 50f;
        rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed * Time.fixedDeltaTime, ForceMode.Force);
    }

    //private void Movement_Performed(InputAction.CallbackContext context)
    //{
    //    //throw new NotImplementedException();
    //    Vector2 inputVector = context.ReadValue<Vector2>();
    //    Debug.Log(context);
    //}

    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Debug.Log("Hey there!");
    }
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
        }
    }

}
