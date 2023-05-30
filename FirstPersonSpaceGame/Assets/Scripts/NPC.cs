using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : Interactable
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject item;
    private Renderer itemRenderer;
    private Color baseColor;
    private bool interacted = false;
    public bool isTalking = false;
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
        if (interacted)
        {
            itemRenderer.material.color = Color.green;
            StartConversation();
        }
        else
        {
            itemRenderer.material.color = baseColor;
            isTalking = false;
        }
    }
    public void StartConversation()
    {
        isTalking = true;
        print("conversation started");

        DialogSystem.Instance.OpenDialogUI();
        DialogSystem.Instance.dialogText.text = "Hello there!";
        DialogSystem.Instance.optionBTN1.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Bye";
        DialogSystem.Instance.optionBTN1.onClick.AddListener(() =>
        {
            DialogSystem.Instance.CloseDialogUI();
            isTalking=false;
        });

    }
}
