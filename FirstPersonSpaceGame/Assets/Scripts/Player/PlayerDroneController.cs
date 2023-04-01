using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDroneController : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    public GroundDroneMotorAI groundDroneMotorAI;
    private bool isFollowing = true;
    private bool isFindingTrees = false;
    void Start()
    {
        inputManager=GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            groundDroneMotorAI.FollowPlayer();
            
        }
        if (!isFindingTrees)
        { 
            groundDroneMotorAI.ResetTrees();
        }
        if (inputManager.onFoot.DroneFollowing.triggered)
        {
            isFollowing = !isFollowing;
            isFindingTrees = !isFindingTrees;
            groundDroneMotorAI.FindTree();
        }
    }
}
