using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public float xSensitivity = 30f;
    public float ySensitivity = 30f;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called before the first frame update
    public void ProcessLook(Vector2 input)
    {
        if (!PlayerInventory.Instance.inventoryOpened && !Chest.Instance.chestOpened && !MenuManager.Instance.isMenuOpened) 
        { 
            float mouseX = input.x;
            float mouseY = input.y;
            //calculate camera rot for looking up and down
            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            //apply to our camera transform
            cam.transform.localRotation=Quaternion.Euler(xRotation, 0f, 0f);
            //rotate player
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
        }
        
    }
}
