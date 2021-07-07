﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class MouseLookNew : MonoBehaviour
{
    public InputMaster controls;
    private static GameObject player;
    private Transform playerBody;

    public float mouseSensitivity = 0f;
    public float gamepadSens = 0f;

    public bool Disabled = false;

    private float xRotation = 0f;

    private void Awake()
    {
        controls.Player.Look.performed += context => updateCameraPos(context.ReadValue<Vector2>(), context.control.device);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void OnDestroy()
    {
        controls.Disable();
        Debug.Log("Controls Disabled Bcecasue Player Was Destroyed");
    }

    void updateCameraPos(Vector2 mouse, UnityEngine.Experimental.Input.InputDevice context)
    {
        if (!Disabled)
        {
            if (context.device is Gamepad)
            {
                mouse.x = mouse.x * gamepadSens;
                mouse.y = mouse.y * gamepadSens;
            }
            else
            {
                mouse.x = mouse.x * mouseSensitivity * Time.deltaTime;
                mouse.y = mouse.y * mouseSensitivity * Time.deltaTime;
            }

            xRotation -= mouse.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouse.x);
        }
    }

    public void spawned()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = player.GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
    }
}
