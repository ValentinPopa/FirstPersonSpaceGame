using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : Interactable
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject item;
    private bool interacted = false;
    public string itemName;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Interact()
    {     
        interacted = !interacted;
        if(interacted)
        {
            //if the inv is not full add the item
            if (!PlayerInventory.Instance.CheckIfFull())
            {
                PlayerInventory.Instance.AddItemToInventory(itemName);
                PlayerInventory.Instance.itemsPickedUp.Add(item.name);
                Destroy(item);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
        }
    }
    public string GetName() 
    {
        return itemName;
    }
}
