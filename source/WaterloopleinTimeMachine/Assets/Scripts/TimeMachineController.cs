using GeoJsonCityBuilder.Components;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TimeMachine))]
public class TimeMachineController : MonoBehaviour
{
    private TimeMachine timeMachine;
    public PlayerInput timeTravelInput;

    void Start()
    {
        timeMachine = GetComponent<TimeMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
