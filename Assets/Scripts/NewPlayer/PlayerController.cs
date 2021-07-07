﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    // These are used to change the movement speed and hold var to the input master and the controller.
    public InputMaster controls;
    public CharacterController controller;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private AnimationCurve crouchCurve;
    float x = 0f;
    float z = 0f;
    bool neverDone = false;
    bool isKeyborad;
    private float inputedMovementSpeed;
    bool sprinting = false;
    bool crouched = false;
    float height = 1.8f;


    // All of the veribles that effects the UI On The Character
    RadialMenu radialMenu;
    [SerializeField] Canvas radialMenuCanvas;
    PauseMenu pauseMenu;
    public bool inUI = false;
    bool gamePaused;

    // These effect how the gravity, ground check and how the jump works.
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpHeight = 3f;
    Vector3 velocity;
    private bool isGrounded;
    private float viberationSpeed = .1f;
    [SerializeField] private bool viberation = false;

    // Store all Scripts that need to be refed on that are on the player
    public Telekinesis telekinesis;
    public CameraRaycast cameraRaycast;
    public Fireball fireball;
    public FireSpray firespray;
    public Freezeshot freezeshot;
    public MouseLookNew mouseLook;
    EventSystem eventSystem;

    // sets inputed movement speed to the inputed speed and sets up all on input performed events.
    private void Awake()
    {

        inputedMovementSpeed = movementSpeed;
        controls.Player.Movement.performed += ctx => updateKeyborad(ctx.ReadValue<Vector2>(), ctx.control.device);
        controls.Player.MovementGamepad.performed += ctx => updateGamepad(ctx.ReadValue<Vector2>(), ctx.control.device);
        controls.Player.Jump.performed += ctx => jump();
        controls.Player.Sprint.started += ctx => toggleSprint();
        controls.Player.Primary.started += ctx => primaryAction();
        controls.Player.Secondary.started += ctx => secondaryActionCharge();
        controls.Player.Secondary.cancelled += ctx => secondartActionRelese();
        controls.Player.Intract.started += ctx => intract();
        controls.Player.Crouch.performed += ctx => crouch();
        controls.Player.OpenRadialMenu.started += ctx => openRadialMenu();
        controls.Player.OpenRadialMenu.cancelled += ctx => closeRadialMenu();
        controls.Player.Pause.performed += ctx => pause();
        controls.Player.UIconfirmation.performed += ctx => Confirmation();
        controls.Player.Look.performed += ctx => radialMenuNav(ctx.ReadValue<Vector2>());

        // Sets all vaules that are required at awake
        telekinesis = GetComponent<Telekinesis>();
        cameraRaycast = GetComponent<CameraRaycast>();
        fireball = GetComponent<Fireball>();
        firespray = GetComponent<FireSpray>();
        freezeshot = GetComponent<Freezeshot>();
        mouseLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLookNew>();

        //Audio Manager
        SoundManager.Initialize();
    }

    private void Start()
    {
        //Sets all vaules for the UI that are required at start.
        radialMenu = GetComponent<RadialMenu>();
        radialMenuCanvas.enabled = false;
        pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
    }

    // When called changes the move of the player based on inputs from the keyborad.
    void updateKeyborad(Vector2 input, UnityEngine.Experimental.Input.InputDevice context)
    {
        isKeyborad = context.device is Keyboard;
        if (isKeyborad == false)
        {
            input.x = 0f;
            input.y = 0f;
        }
        else
        {

            x = input.x;
            z = input.y;
        }
        
        if (input.x < 0f || input.y < 0f)
        {
            neverDone = false;
        }
        else if (input.x > 0f || input.y > 0f)
            neverDone = false;



        if (input.x == 0 && input.y == 0)
        {
            isKeyborad = false;

        }

        bool isIdle = x == 0 && z == 0;
        if (!isIdle) {
            SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
        }
    }
    
    // When called changes the move of the player based on inputs from the Gamepad.
    void updateGamepad(Vector2 input, UnityEngine.Experimental.Input.InputDevice context)
    {
        bool isGamepad = false;
        if (input.x == 0 && input.y == 0)
        {
            if (!isKeyborad)
            {
                if (!neverDone)
                {
                    neverDone = true;
                    isGamepad = false;
                }
            }
        }
        else
        {
            isGamepad = context.device is Gamepad;
        }

        if (input.x < 0f || input.y < 0f)
        {
            neverDone = false;
        }
        else if (input.x > 0f || input.y > 0f)
            neverDone = false;

        if (isGamepad == false)
        {
            input.x = 0f;
            input.y = 0f;
        }
        else
        {
            x = input.x;
            z = input.y;
        }
    }

    // When called launches the player into the air.
    void jump()
    {
        if (!inUI)
        {
            if (isGrounded)
            {                
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }

    // When called toggles sprint on and off.
    void toggleSprint()
    {
        if (!sprinting)
        {
            movementSpeed = sprintSpeed;
            sprinting = true;
        }
        else if (sprinting)
        {
            movementSpeed = inputedMovementSpeed;
            sprinting = false;
        }
    }

    void crouch()
    {
        if (crouched)
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime;
                height = crouchCurve.Evaluate(t);
                controller.height = height;
            }
            crouched = false;
        }
        else if (!crouched)
        {
            float t = 1f;

            while (t > 0f)
            {
                t -= Time.deltaTime;
                height = crouchCurve.Evaluate(t);
                controller.height = height;
            }
            crouched = true;
        }
    }

    // When called based on what spell is currently active and does the corret action.
    public void primaryAction()
    {
        if (telekinesis.enabled == true)
        {
            if (telekinesis.hasObject == false)
            {
                telekinesis.doRay();
            }
            else if (telekinesis.hasObject == true)
            {
                telekinesis.dropObject();
            }
        }
        else if (firespray.enabled == true)
        {
            
            firespray.FireTest();
            Debug.Log("FireSpray");
        }
        else if (fireball.enabled == true)
        {
            fireball.FireTest();
            SoundManager.PlaySound(SoundManager.Sound.PlayerFire);
            Debug.Log("Fireball");
        }
        else if (freezeshot.enabled == true)
        {
            Debug.Log("FreezeShot");
            freezeshot.FireTest();
        }
    }

    // When called if the current spell has a alt action will then proform it.
    void secondaryActionCharge()
    {
        if (telekinesis.enabled == true)
        {
            if (telekinesis.enabled == true && telekinesis.hasObject == true)
            {
                telekinesis.shouldCharge = true;
                telekinesis.updateForce();
                viberation = true;
            }
        }
        else if (fireball.enabled == true)
        {
            
        }
    }

    // Called when the charge for the alt action is done
    void secondartActionRelese()
    {
        if (telekinesis.enabled == true && telekinesis.hasObject == true)
        {
            telekinesis.shouldCharge = false;
            telekinesis.shootObject();
            Gamepad.current.SetMotorSpeeds(0f, 0f);
            viberationSpeed = .1f;
            viberation = false;
        }
    }

    // When called intracts with the current intractable object in front of the camera.
    void intract()
    {
        if (cameraRaycast.currentTarget != null)
        {
            cameraRaycast.currentTarget.OnIntract();
        }
    }

    //When called toggles the radial menu on and off
    void openRadialMenu()
    {
        Debug.Log("opened radial menu");
        if (!inUI)
        {
            radialMenuCanvas.enabled = true;
            radialMenu.Open();
            mouseLook.Disabled = true;
            inUI = true;
        }
    }

    void closeRadialMenu()
    {
        Debug.Log("closed radial menu");
        if (inUI)
        {
            radialMenuCanvas.enabled = false;
            radialMenu.Close();
            mouseLook.Disabled = false;
            inUI = false;
            radialMenu.MousePos.x = 0f;
            radialMenu.MousePos.y = 0f;
        }
    }

    void radialMenuNav(Vector2 loc)
    {
        if (inUI)
        {
            radialMenu.MousePos.x = loc.x;
            radialMenu.MousePos.y = loc.y;
            //(Screen.width / 2f);
        }
    }

    // When called toggles between having the game paused and unpaused
    void pause()
    {
        if (pauseMenu.GameIsPaused)
        {
            pauseMenu.Resume();
            inUI = false;
        }
        else
        {
            pauseMenu.Pause();
            inUI = true;
        }
    }

    // When the player press the south gamepad button this is called which then tells the event system to trigger teh selected button.
    void Confirmation()
    {
        if (inUI)
        {
            if (radialMenu.open == true)
            {
                radialMenu.ButtonAction(radialMenu.CurrentSpell);
            }
            else if (pauseMenu.GameIsPaused)
            {
                GameObject currentlySelected = eventSystem.currentSelectedGameObject;
                ExecuteEvents.Execute(currentlySelected, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
        }
    }

    // This are used to enable and disable the controls.
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    // Updates the player location, which state they are in (E.G. using gamepad) and also applies gravity to the player.
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        

        if (neverDone)
        {
            
            x = 0f;
            z = 0f;
            movementSpeed = inputedMovementSpeed;
            sprinting = false;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (viberation)
        {
            Gamepad.current.SetMotorSpeeds(viberationSpeed, viberationSpeed);
            viberationSpeed += .005f;
            Debug.Log(viberationSpeed);
        }
    }
}