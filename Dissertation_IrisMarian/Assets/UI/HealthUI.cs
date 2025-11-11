using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider health3DSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start3DSlider(float maxValue)
    {
        health3DSlider.maxValue = maxValue;
        health3DSlider.value = maxValue;
    }

    // Update is called once per frame
    public void Update3DSlider(float value)
    {
        health3DSlider.value = value;
    }
}
