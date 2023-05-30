using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDroneController : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    public GroundDroneMotorAI groundDroneMotorAI;
    private bool isFollowing = true;
    private bool isFindingNPCS = false;
    public Button saraButton;
    public GameObject droneNPCSUI;
    void Start()
    {
        inputManager=GetComponent<InputManager>();
        saraButton = droneNPCSUI.transform.Find("SaraButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            groundDroneMotorAI.FollowPlayer();            
        }
        if (!isFindingNPCS)
        { 
            groundDroneMotorAI.ResetNPCS();
        }
        if (inputManager.onFoot.DroneFollowing.triggered)
        {
            if (!isFindingNPCS)
            {
                droneNPCSUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                saraButton.onClick.AddListener(() =>
                {
                    string npcTag = "Sara";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                });
                
            }
            isFollowing = !isFollowing;
            isFindingNPCS = !isFindingNPCS;
        }
    }
}
