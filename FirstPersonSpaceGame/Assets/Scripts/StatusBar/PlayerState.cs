using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance { get; set; }
    public GameObject statusBarUI;
    //PlayerHealth
    public float currentHealth;
    public float maxHealth;
    //PlayerFood
    public float currentFood;
    public float maxFood;
    float distanceTraveled=0;
    Vector3 lastPosition;
    public GameObject player;
    //PlayeWater
    public float currentWater;
    public float maxWater;
    public bool isWaterTriggered=true;

    private void Awake()
    {
        if(instance!=null && instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        statusBarUI.SetActive(true);
        currentHealth = maxHealth; 
        currentFood = maxFood;
        currentWater = maxWater;
        StartCoroutine(decreaseWater());
    }
    IEnumerator decreaseWater()
    {
        while(true)
        {
            currentWater--;
            yield return new WaitForSeconds(10);
        }
    }
    void Update()
    {
        distanceTraveled += Vector3.Distance(player.transform.position, lastPosition);
        lastPosition=player.transform.position;
        if(distanceTraveled>=15)
        {
            distanceTraveled = 0;
            currentFood--;
        }
    }
}
