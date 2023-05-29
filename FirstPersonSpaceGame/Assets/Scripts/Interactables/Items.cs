using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : Interactable
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject item;
    private Renderer itemRenderer;
    private Color baseColor;
    private bool interacted = false;
    public string itemName;


    void Start()
    {
        itemRenderer = item.GetComponent<Renderer>();
        baseColor = itemRenderer.material.color;
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
                itemRenderer.material.color = baseColor;        
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
