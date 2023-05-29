using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; set; }
    public GameObject inventoryScreenUI;
    private InputManager inputManager;
    public bool inventoryOpened = false;

    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();
    private GameObject itemToAdd;
    private GameObject whichSlotToEquip;
    // public bool isFull;


    public GameObject pickUpPopUp;
    public Text pickUpName;
    public Image pickUpImage;

    public GameObject ItemInfoUI;

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
        //isFull = false;
        PopulateSlotList();
        Debug.Log(slotList.Count);
    }

    private void PopulateSlotList()
    {
        foreach(Transform child in inventoryScreenUI.transform)
        {
            if(child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (inputManager.onFoot.Inventory.triggered)
        {
            inventoryOpened = !inventoryOpened;
            if (inventoryOpened)
            {
                Cursor.lockState = CursorLockMode.None;
                inventoryScreenUI.SetActive(true);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                inventoryScreenUI.SetActive(false);
            }
        }
    }

    public void AddItemToInventory(string itemName)
    {
        whichSlotToEquip = FindNextEmptySlot();
        itemToAdd=(GameObject)Instantiate(Resources.Load<GameObject>(itemName),whichSlotToEquip.transform.position,whichSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whichSlotToEquip.transform);        
        itemList.Add(itemName);
        Sprite itemSprite=itemToAdd.GetComponent<Image>().sprite;

        StartCoroutine(ActivatePickUpPopUp(itemName, itemSprite));
    }

    private IEnumerator ActivatePickUpPopUp(string itemName, Sprite itemSprite)
    {
        pickUpPopUp.SetActive(true);
        pickUpName.text = itemName;
        pickUpImage.sprite = itemSprite;

        yield return new WaitForSeconds(1f); // Wait for 3 seconds

        pickUpPopUp.SetActive(false); // Deactivate the pop-up
    }


    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in slotList) 
        {
            if(slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {
        int counter = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter++;
            }
            if(counter == slotList.Count)
            {
                return true;
            }    
        }
        return false;
    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;
        for(var i=slotList.Count-1;i>=0;i--)
        {
            if (slotList[i].transform.childCount>0)
            {
                if (slotList[i].transform.GetChild(0).name==nameToRemove+"(Clone)" && counter!=0)
                {
                    DestroyImmediate(slotList[i].transform.GetChild(0).gameObject);
                    counter--;
                }
            }
        }
        ReCalculateList();
        //CraftingSystem.Instance.RefreshNeededItems();
    }
    public void ReCalculateList()
    {
        itemList.Clear();
        foreach(GameObject slot in slotList)
        {
            if(slot.transform.childCount>0)
            {
                string name=slot.transform.GetChild(0).name;
                string str2 = "(Clone)";
                string result = name.Replace(str2, "");
                itemList.Add(result);
            }
        }
    }
}