using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDroneController : MonoBehaviour
{
    public static PlayerDroneController Instance { get; set; }
    // Start is called before the first frame update
    private InputManager inputManager;
    public GroundDroneMotorAI groundDroneMotorAI;
    private bool isFollowing = true;
    public bool isFindingNPCS = false;
    public bool isFindNPCSMenuOpened;
    public Button saraButton;
    public Button andrewButton;
    public Button johnButton;
    public Button dukyButton;
    public Button gingerButton;
    public Button arizonaButton;
    public Button netyButton;
    public Button nidusButton;
    public Button leonaButton;
    public Button luluButton;
    public Button dariusButton;

    public GameObject droneNPCSUI;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
        inputManager=GetComponent<InputManager>();
        saraButton = droneNPCSUI.transform.Find("SaraButton").GetComponent<Button>();
        andrewButton = droneNPCSUI.transform.Find("AndrewButton").GetComponent<Button>();
        johnButton = droneNPCSUI.transform.Find("JohnButton").GetComponent<Button>();
        dukyButton = droneNPCSUI.transform.Find("DukyButton").GetComponent<Button>();
        gingerButton = droneNPCSUI.transform.Find("GingerButton").GetComponent<Button>();
        arizonaButton = droneNPCSUI.transform.Find("ArizonaButton").GetComponent<Button>();
        netyButton = droneNPCSUI.transform.Find("NetyButton").GetComponent<Button>();
        nidusButton = droneNPCSUI.transform.Find("NidusButton").GetComponent<Button>();
        leonaButton = droneNPCSUI.transform.Find("LeonaButton").GetComponent<Button>();
        luluButton = droneNPCSUI.transform.Find("LuluButton").GetComponent<Button>();
        dariusButton = droneNPCSUI.transform.Find("DariusButton").GetComponent<Button>();
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
                isFindNPCSMenuOpened = true;

                saraButton.onClick.AddListener(() =>
                {
                    string npcTag = "Sara";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                andrewButton.onClick.AddListener(() =>
                {
                    string npcTag = "Andrew";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                johnButton.onClick.AddListener(() =>
                {
                    string npcTag = "John";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                dukyButton.onClick.AddListener(() =>
                {
                    string npcTag = "Duky";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                gingerButton.onClick.AddListener(() =>
                {
                    string npcTag = "Ginger";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                arizonaButton.onClick.AddListener(() =>
                {
                    string npcTag = "Arizona";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                netyButton.onClick.AddListener(() =>
                {
                    string npcTag = "Nety";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                nidusButton.onClick.AddListener(() =>
                {
                    string npcTag = "Nidus";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                leonaButton.onClick.AddListener(() =>
                {
                    string npcTag = "Leona";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                luluButton.onClick.AddListener(() =>
                {
                    string npcTag = "Lulu";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

                dariusButton.onClick.AddListener(() =>
                {
                    string npcTag = "Darius";
                    groundDroneMotorAI.FindNPCS(npcTag);
                    droneNPCSUI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    isFindNPCSMenuOpened = false;
                });

            }
            isFollowing = !isFollowing;
            isFindingNPCS = !isFindingNPCS;
        }
    }
}
