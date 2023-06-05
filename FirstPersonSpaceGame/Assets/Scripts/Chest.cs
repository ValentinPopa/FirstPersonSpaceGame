using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : Interactable
{
    // Start is called before the first frame update
    public static Chest Instance { get; set; }
    public bool chestOpened = false;
    public GameObject chestInventoryScreenUI;
    public GameObject inventoryScreenUI;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;

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
        chestOpened= false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in chestInventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(chestOpened && DragDrop.itemBeingDragged!=null)
        {
            string name=DragDrop.itemBeingDragged.name;
            itemList.Add(name);
            PlayerInventory.Instance.itemList.Remove(name);
        }
    }

    protected override void Interact()
    {
        chestOpened = !chestOpened;
        if (chestOpened)
        {
            Cursor.lockState = CursorLockMode.None;
            chestInventoryScreenUI.SetActive(true);
            inventoryScreenUI.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            chestInventoryScreenUI.SetActive(false);
            inventoryScreenUI.SetActive(false);
        }
    }

    /*public void AddItemToChestInventory(string itemName)
    {      
        itemToAdd.transform.SetParent(whichSlotToEquip.transform);
        itemList.Add(itemName);
    }*/
}
