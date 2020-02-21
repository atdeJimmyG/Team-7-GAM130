using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class MouseLook : MonoBehaviour
{
    public InputMaster controls;
    public Transform playerBody;

    public float mouseSensitivity = 0f;
    public float gamepadSens = 0f;

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

    void updateCameraPos(Vector2 mouse, UnityEngine.Experimental.Input.InputDevice context)
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
