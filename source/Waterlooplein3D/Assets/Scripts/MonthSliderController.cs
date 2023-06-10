using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeoJsonCityBuilder;


public class MonthSliderController : MonoBehaviour
{
    Slider m_Slider;
    AutomaticSunPosition m_SunPosition;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Slider = GetComponent<Slider>();
        m_SunPosition = GameObject.FindObjectOfType<AutomaticSunPosition>();

        //Add listener for when the state of the Toggle changes, to take action
        m_Slider.onValueChanged.AddListener(delegate {
            SliderValueChanged(m_Slider);
        });

        SliderValueChanged(m_Slider);
    }

    //Output the new state of the Toggle into Text
    void SliderValueChanged(Slider change)
    {
        m_SunPosition.month = (int)change.value;
    }
}
