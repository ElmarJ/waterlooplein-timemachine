using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GeoJsonCityBuilder;


public class TimeSliderController : MonoBehaviour
{
    Slider m_Slider;
    TimeMachine m_TimeMachine;

    void Start()
    {
        //Fetch the Toggle GameObject
        m_Slider = GetComponent<Slider>();
        m_TimeMachine = GameObject.FindObjectOfType<TimeMachine>();

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
    }
}
