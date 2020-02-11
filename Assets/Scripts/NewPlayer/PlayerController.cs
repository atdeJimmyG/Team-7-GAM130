using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerController : MonoBehaviour
{
    public InputMaster controls;
    public CharacterController controller;
    [SerializeField]private  float movementSpeed = 12f;

    private Vector2 movementInput;
    private void Awake()
    {
        controls.Player.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Player.Movement.performed += ctx => debug(ctx.ReadValue<Vector2>(), ctx.control.device);
    }

    void debug(Vector2 input, UnityEngine.Experimental.Input.InputDevice context)
    {
        bool isKeyborad = context.device is Keyboard;

        if (isKeyborad)
            Debug.Log(input);
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        float x = movementInput.x;
        float z = movementInput.y; 
       
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * movementSpeed * Time.deltaTime);
    }
}

