using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    private Slider slider;
    public Text waterCounter;

    public GameObject playerState;

    private float currentWater;
    private float maxWater;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }


    void Update()
    {
        currentWater = playerState.GetComponent<PlayerState>().currentWater;
        maxWater = playerState.GetComponent<PlayerState>().maxWater;
        float fillValue = currentWater / maxWater; //divison bcs slider has values between 0-1
        slider.value = fillValue;
        waterCounter.text = currentWater + "/" + maxWater;
    }
}
