using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectWithTag("damga").GetComponent<PlayerController>().startGame == false)
        {
            slider.maxValue = PlayerPrefs.GetInt("damgaHakki");
      
        }

        slider.value = PlayerPrefs.GetInt("damgaHakki");



    }
}
