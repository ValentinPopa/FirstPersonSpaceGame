using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodBar : MonoBehaviour
{
    private Slider slider;
    public Text foodCounter;

    public GameObject playerState;

    private float currentFood;
    private float maxFood;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }


    void Update()
    {
        currentFood = playerState.GetComponent<PlayerState>().currentFood;
        maxFood = playerState.GetComponent<PlayerState>().maxFood;
        float fillValue = currentFood / maxFood; //divison bcs slider has values between 0-1
        slider.value = fillValue;
        foodCounter.text = currentFood + "/" + maxFood;
    }
}
