using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    private GroundDroneMotor groundDroneMotor;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float crouchTimer=30f;
    public bool crouching; 
    public bool sprinting;
    public bool lerpCrouch;
    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CharacterController>();
        groundDroneMotor= gameObject.AddComponent<GroundDroneMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded=controller.isGrounded;

        float p = crouchTimer / 1;
        p *= p;
        if (crouching)
        {
            controller.height = Mathf.Lerp(controller.height, 1f, p);
        }
        else
        {
            controller.height = Mathf.Lerp(controller.height, 2f, p);
        }
        if(p > 1)
        {
            lerpCrouch = false;
            crouchTimer = 0f;
        }
    }
    //gets the inputs from InputManager.cs and applies them to out character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection)*speed*Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);

    }
    public void Jump()
    {
        if(isGrounded) 
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }
    public void Sprint()
    {
        sprinting = !sprinting;
        if(sprinting)
        {
            speed = 8;
        }
        else
        {
            speed = 5;
        }
    }
}
