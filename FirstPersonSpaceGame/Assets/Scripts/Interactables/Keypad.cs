using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Interactable
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject button;
    private bool doorOpen = false;
    private Renderer itemRenderer;
    private Color baseColor;

    void Start()
    {
        itemRenderer = button.GetComponent<Renderer>();
        baseColor = itemRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {    
        if (doorOpen)
        {
            itemRenderer.material.color = baseColor;
        }
        else
        {
            itemRenderer.material.color = Color.green;
        }
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }
}
