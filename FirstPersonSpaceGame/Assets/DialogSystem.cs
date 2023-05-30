using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static DialogSystem Instance { get; set; }
    public TextMeshProUGUI dialogText;
    public Button optionBTN1;
    public Button optionBTN2;
    public GameObject dialogUI;
    public bool dialogOpened;

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

    public void OpenDialogUI()
    {
        dialogUI.SetActive(true);
        dialogOpened = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void CloseDialogUI()
    {
        dialogUI.SetActive(false);
        dialogOpened = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
