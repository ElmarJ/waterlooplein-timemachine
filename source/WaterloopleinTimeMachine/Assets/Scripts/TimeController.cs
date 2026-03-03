using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TimeController : MonoBehaviour
{
    public UnityEvent OnYearChanged;
    public UnityEvent OnMonthChanged;
    public UnityEvent OnDayChanged;
    public UnityEvent OnTimeChanged;

    public int year;
    public int month;
    public int day;
    public int hour;

    public DateTime Date => new(year, month, day, hour, 0, 0);

    public InputActionReference changeYearAction;
    public InputActionReference changeYearFastAction;

    private int lastYear;
    private int lastMonth;
    private int lastDay;
    private int lastTime;

    private bool fastChangeActive = false;

    void Update()
    {
        if (changeYearAction.action.triggered)
        {
            var yearSlowChangeInput = changeYearAction.action.ReadValue<float>();
            year += (int)yearSlowChangeInput;
        }
        
        if(changeYearFastAction.action.triggered)
        {
            fastChangeActive = true;
        }
        else if (fastChangeActive && changeYearFastAction.action.IsInProgress() == false)
        {
            fastChangeActive = false;
        }
        if(fastChangeActive)
        {
            // TODO: read the input value as a float and apply it continuously with month-long steps

            // Continuous fast change while the action is active
            var yearFastChangeInput = changeYearFastAction.action.ReadValue<float>();
            year += (int) (yearFastChangeInput *  Time.deltaTime);

        }

        if (lastYear != year)
        {
            OnYearChanged?.Invoke();
            lastYear = year;
        }
        if (lastMonth != month)
        {
            OnMonthChanged?.Invoke();
            lastMonth = month;
        }
        if (lastDay != day)
        {
            OnDayChanged?.Invoke();
            lastDay = day;
        }
        if (lastTime != hour)
        {
            OnTimeChanged?.Invoke();
            lastTime = hour;
        }
    }
}