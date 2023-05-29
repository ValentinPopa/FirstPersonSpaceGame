using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; set; }
    private InputManager inputManager;
    public GameObject menuCanvas;
    public GameObject uiCanvas;
    public GameObject saveMenu;
    public GameObject settingsMenu;
    public GameObject menu;

    public bool isMenuOpened=false;

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
    private void Start()
    {
        inputManager = GetComponent<InputManager>();
    }
    private void Update()
    {
        if(inputManager.onFoot.InGameMenu.triggered)
        {
            isMenuOpened = !isMenuOpened;
            if (isMenuOpened)
            {
                uiCanvas.SetActive(false);
                menuCanvas.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
            }
            else
            {
                saveMenu.SetActive(false);
                settingsMenu.SetActive(false);
                menu.SetActive(true);

                uiCanvas.SetActive(true);
                menuCanvas.SetActive(false);
                if(!PlayerInventory.Instance.inventoryOpened && !Chest.Instance.chestOpened)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                
                //Cursor.visible = false;
            }
        }
    }
}
