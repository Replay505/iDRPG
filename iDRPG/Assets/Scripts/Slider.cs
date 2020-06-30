using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{

    public Image fillimage;
    private Slider slider;


    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float fillValue = playerHealth.currentHealth / 10;
        slider.value = fillValue;
    }
}
