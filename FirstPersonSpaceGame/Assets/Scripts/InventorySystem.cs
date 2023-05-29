using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; set; }
    public GameObject inventoryScreenUI;
    private InputManager inputManager;
    private bool inventoryOpened = false;
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
        inventoryOpened = false;
        inputManager = GetComponent<InputManager>();
    }
    void Update()
    {
        if (inputManager.onFoot.Inventory.triggered)
        {
            inventoryOpened = !inventoryOpened;
            if (inventoryOpened)
            {
                Debug.Log("i is pressed");
                inventoryScreenUI.SetActive(true);
            }
            else
            {
                Debug.Log("Ies");
                inventoryScreenUI.SetActive(false);
            }
        }
    }
}