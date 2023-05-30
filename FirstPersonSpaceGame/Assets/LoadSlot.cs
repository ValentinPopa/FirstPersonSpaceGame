using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadSlot : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI buttonText;
    public int slotNumber;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (!SaveManager.Instance.IsSlotEmpty(slotNumber))
            {
                SaveManager.Instance.StartLoadedGame(slotNumber);
                SaveManager.Instance.DeselectButton();
            }
            else
            {
                //display override warning
            }
        });
    }
    private void Update()
    {
        if (SaveManager.Instance.IsSlotEmpty(slotNumber))
        {
            buttonText.text = "";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Description");
        }
    }
}
