using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class EscapePlanet : Interactable
{
    // Start is called before the first frame update
    public static EscapePlanet Instance { get; set; }
    public GameObject button;
    private Renderer itemRenderer;
    private Color baseColor;
    public int questsNeeded;
    public int questsCompleted;
    public GameObject numberOfQuestsInfoUI;
    private Text textToModify;
    public List<string> npcDespawned;

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
        itemRenderer = button.GetComponent<Renderer>();
        baseColor = itemRenderer.material.color;
        textToModify = numberOfQuestsInfoUI.transform.Find("NumberOfQuestsInfo").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    protected override void Interact()
    {
        if (questsCompleted==questsNeeded)
        {
            itemRenderer.material.color = baseColor;
        }
        else
        {
            StartCoroutine(NotifyNumberOfQuests());
            itemRenderer.material.color = Color.green;
        }
    }
    IEnumerator NotifyNumberOfQuests()
    {
        numberOfQuestsInfoUI.SetActive(true);
        textToModify.text = "You need " + (questsNeeded - questsCompleted) + " in order to access the spaceship!";
        yield return new WaitForSeconds(3f);
        numberOfQuestsInfoUI.SetActive(false);
    }
}
