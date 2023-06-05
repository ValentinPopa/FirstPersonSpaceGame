using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class VerifyQuestItem : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject verifyQuestItemUI;
    public GameObject questItemUI;
    private Text textToModify;

    private Image imageComponent;
    public TextMeshProUGUI questItem;
    public TextMeshProUGUI npcName;

    Button YesBTN, NoBTN;

    GameObject draggedItem
    {
        get
        {
            return DragDrop.itemBeingDragged;
        }
    }

    public bool isItemGood;
    GameObject itemToBeDeleted;

    public string itemName
    {
        get
        {
            string name = itemToBeDeleted.name;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }

    void Start()
    {
        imageComponent = transform.Find("verify").GetComponent<Image>();

        textToModify = verifyQuestItemUI.transform.Find("Sentence").GetComponent<Text>();

        YesBTN = verifyQuestItemUI.transform.Find("Complete").GetComponent<Button>();
        YesBTN.onClick.AddListener(delegate { DeleteItem(); });

        NoBTN = verifyQuestItemUI.transform.Find("Retry").GetComponent<Button>();
        NoBTN.onClick.AddListener(delegate { CancelDeletion(); });
    }


    public void OnDrop(PointerEventData eventData)
    {
        //itemToBeDeleted = DragDrop.itemBeingDragged.gameObject;
        if (draggedItem.GetComponent<InventoryItem>().isQuestItem == true)
        {
            itemToBeDeleted = draggedItem.gameObject;

            StartCoroutine(notifyBeforeVerification());
        }
    }
    
    IEnumerator notifyBeforeVerification()
    {
        verifyQuestItemUI.SetActive(true);
        print(questItem.text);
        print(itemName);
        print(npcName.text);
        if(itemName==questItem.text)
        {
            textToModify.text = "Press complete to complete the quest!";
            isItemGood = true;
        }
        else
        {
            textToModify.text = "Press retry to insert another object!";
            isItemGood = false;
        }
        yield return new WaitForSeconds(1f);
    }

    private void CancelDeletion()
    {
        //imageComponent.sprite = trash_closed;
        verifyQuestItemUI.SetActive(false);
    }

    private void DeleteItem()
    {
        //imageComponent.sprite = trash_closed;
        DestroyImmediate(itemToBeDeleted.gameObject);
        PlayerInventory.Instance.ReCalculateList();
        //CraftingSystem.Instance.RefreshNeededItems();
        EscapePlanet.Instance.questsCompleted++;
        verifyQuestItemUI.SetActive(false);
        questItemUI.SetActive(false);

        GameObject obj = GameObject.Find(npcName.text);

        if (obj != null)
        {
            Destroy(obj);
        }
        else
        {
            Debug.LogWarning("Object not found: " + npcName.text);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            //imageComponent.sprite = trash_opened;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            // imageComponent.sprite = trash_closed;
        }
    }

}