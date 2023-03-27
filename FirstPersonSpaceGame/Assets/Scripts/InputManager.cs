using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    public object OnFoot { get; internal set; }

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot=playerInput.OnFoot;

        motor=GetComponent<PlayerMotor>();
        look=GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();//callback context

        onFoot.Crouch.performed += ctx => motor.Crouch();

        onFoot.Sprint.performed += ctx => motor.Sprint();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
