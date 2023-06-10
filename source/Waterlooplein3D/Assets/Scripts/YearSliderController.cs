using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeoJsonCityBuilder;


public class YearSliderController : MonoBehaviour
{
    Slider m_Slider;
    TimeMachine m_TimeMachine;
    AutomaticSunPosition m_SunPosition;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Slider = GetComponent<Slider>();
        m_TimeMachine = GameObject.FindObjectOfType<TimeMachine>();
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
        m_TimeMachine.year = (int)change.value;
        m_SunPosition.year = (int)change.value;
    }
}
