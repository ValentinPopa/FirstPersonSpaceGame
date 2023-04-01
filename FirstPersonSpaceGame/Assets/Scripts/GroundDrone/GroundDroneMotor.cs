using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDroneMotor : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    public GameObject player;
    private CharacterController controller;
    public float distanceToPlayer = 5f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
    }

    public void Follow()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        Vector3 targetPosition = player.transform.position - direction * distanceToPlayer;
        Vector3 moveDirection = targetPosition - transform.position;
        moveDirection.Normalize();
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}
