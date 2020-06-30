using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider;
    void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void SetHealth(int x)
    {
        healthSlider.value = x;
    }
}
