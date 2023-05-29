using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }
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
        if(Instance!=null && Instance !=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
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
    public void setHealth(float newHealth)
    {
        currentHealth = newHealth;
    }
    public void setFood(float newFood)
    {
        currentFood = newFood;
    }
    public void setWater(float newWater)
    {
        currentWater = newWater;
    }
}
