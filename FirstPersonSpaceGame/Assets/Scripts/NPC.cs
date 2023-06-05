using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPC : Interactable
{
    // Start is called before the first frame update

    [SerializeField]
    private bool interacted = false;
    public bool isTalking = false;
    public TextMeshProUGUI dialogText;
    public string dialogTextString;
    public Button optionBTN1;
    public Button optionBTN2;
    public GameObject dialogUI;
    public bool dialogOpened;
    public GameObject questUI;
    public string questItemName;
    public TextMeshProUGUI questText;
    public TextMeshProUGUI questItem;
    public bool questCompleted;
    public TextMeshProUGUI npcName;
    public string NPCName;
    void Start()
    {

    }
    public string QuestItemName()
    {
        return questItemName;
    }
    // Update is called once per frame
    void Update()
    {

    }
    protected override void Interact()
    {
        interacted = !interacted;
        if (interacted)
        {
            StartConversation();
        }
        else
        {
            isTalking = false;
            CloseDialogUI();
        }
    }

    public void StartConversation()
    {
        isTalking = true;
        print("conversation started");

        OpenDialogUI();
        dialogText.text = dialogTextString;
        optionBTN1.transform.Find("Bye").GetComponent<TextMeshProUGUI>().text = "Bye";
        optionBTN1.onClick.AddListener(() =>
        {
            CloseDialogUI();
            isTalking=false;
            interacted = !interacted;
        });
        optionBTN2.transform.Find("AcceptQuest").GetComponent<TextMeshProUGUI>().text = "Accept quest";
        optionBTN2.onClick.AddListener(() =>
        {
            CloseDialogUI();
            OpenQuestUI();
            isTalking = false;
            interacted = !interacted;
        });
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
        //Cursor.visible = false;
    }
    public void OpenQuestUI()
    {
        questUI.SetActive(true);
        questText.text = "You have to bring me the following item:";
        questItem.text = questItemName;
        npcName.text = NPCName;
    }
}
