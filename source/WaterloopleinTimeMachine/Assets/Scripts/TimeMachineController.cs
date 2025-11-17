using GeoJsonCityBuilder.Components;
using UnityEngine;
using UnityEngine.InputSystem;

// Automatically syncs the Time Machine component with the Time Controller component.

[RequireComponent(typeof(TimeMachine))]
public class TimeMachineController : MonoBehaviour
{
    public TimeController timeController;
    private TimeMachine timeMachine;

    void Start()
    {
        timeMachine = GetComponent<TimeMachine>();
        timeController.OnYearChanged.AddListener(UpdateTimeMachine);
    }

    public void UpdateTimeMachine()
    {
        timeMachine.year = timeController.year;
    }
}
