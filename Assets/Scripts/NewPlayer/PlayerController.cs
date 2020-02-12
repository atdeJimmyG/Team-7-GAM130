using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Input;

public class PlayerController : MonoBehaviour
{
    // These are used to change the movement speed and hold var to the input master and the controller.
    public InputMaster controls;
    public CharacterController controller;
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float sprintSpeed = 20f;
    float x = 0f;
    float z = 0f;
    bool neverDone = false;
    bool isKeyborad;
    private float inputedMovementSpeed;
    bool sprinting = false;

    // These effect how the gravity, ground check and how the jump works.
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float jumpHeight = 3f;
    Vector3 velocity;
    private bool isGrounded;


    // Store all Scripts that need to be refed on that are on the player
    public Telekinesis telekinesis;
    public CameraRaycast cameraRaycast;
    public Fireball fireball;

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

        // Sets all vaules that are required at awake
        telekinesis = this.GetComponent<Telekinesis>();
        cameraRaycast = this.GetComponent<CameraRaycast>();
        fireball = this.GetComponent<Fireball>();
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
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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

    // When called based on what spell is currently active and does the corret action.
    void primaryAction()
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
        else if (fireball.enabled == true)
        {

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
    }
}

