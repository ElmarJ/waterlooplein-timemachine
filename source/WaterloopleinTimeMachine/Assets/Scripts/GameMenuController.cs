using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Linq;
using System;
using GeoJsonCityBuilder;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameMenuController : MonoBehaviour
{
    public GameObject player;
    public UIDocument mainMenu;
    public UIDocument settingsMenu;

    private PlayerInput playerInput;

    private SliderInt yearSlider;
    private TimeMachine timeMachine;
    private AutomaticSunPosition sunPosition;

    private readonly string[] monthNames = { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

    public void OnEnable()
    {
        this.playerInput = player.GetComponent<PlayerInput>();
        this.timeMachine = GameObject.FindObjectOfType<TimeMachine>();
        this.sunPosition = GameObject.FindObjectOfType<AutomaticSunPosition>();

        this.ActivateMenu();        
        this.ShowMainMenu();
    }

    // TODO: consider using a state machine that keeps track of the current menu (or general state of the game)
 
    public void ActivateMenu()
    {   
        this.SetMenuMouseState(true);
        this.ShowMainMenu();
    }

    public void ResumeGame()
    {
        this.SetMenuMouseState(false);
        this.HideAllMenus();
    }

    public void SetMenuMouseState(bool mouseHasMenuState)
    {
        UnityEngine.Cursor.lockState = mouseHasMenuState ? CursorLockMode.None : CursorLockMode.Locked; 
        UnityEngine.Cursor.visible = mouseHasMenuState;
        if(mouseHasMenuState)
        {
            this.playerInput.actions.Disable();
        }
        else
        {
            this.playerInput.actions.Enable();
        }
        this.player.GetComponent<FirstPersonDrifter>().enabled = !mouseHasMenuState;
    }

    public void ShowSettingsMenu()
    {
        this.mainMenu.enabled = false;
        this.settingsMenu.enabled = true;

        this.settingsMenu.rootVisualElement.Q<Button>("back-button").RegisterCallback<ClickEvent>(ev => this.ShowMainMenu());

        var qualityDropdown = this.settingsMenu.rootVisualElement.Q<DropdownField>("quality-dropdown");
        qualityDropdown.choices = new(QualitySettings.names);
        qualityDropdown.index = QualitySettings.GetQualityLevel();
        qualityDropdown.RegisterValueChangedCallback(ev => QualitySettings.SetQualityLevel(qualityDropdown.index));

        var resolutionDropdown = this.settingsMenu.rootVisualElement.Q<DropdownField>("resolution-dropdown");
        resolutionDropdown.choices = (from resolution in Screen.resolutions select $"{resolution.width} x {resolution.height}").ToList();
        resolutionDropdown.index = GetResolutionIndex();
        resolutionDropdown.RegisterValueChangedCallback(ev => Screen.SetResolution(Screen.resolutions[resolutionDropdown.index].width, Screen.resolutions[resolutionDropdown.index].height, true));
    }

    public void ShowMainMenu()
    {
        this.mainMenu.enabled = true;
        this.settingsMenu.enabled = false;

        this.mainMenu.rootVisualElement.Q<Button>("resume-button").RegisterCallback<ClickEvent>(ev => this.ResumeGame());
        this.mainMenu.rootVisualElement.Q<Button>("settings-button").RegisterCallback<ClickEvent>(ev => this.ShowSettingsMenu());
        this.mainMenu.rootVisualElement.Q<Button>("exit-button").RegisterCallback<ClickEvent>(ev => this.ExitGame());

        UpdateYear(this.mainMenu.rootVisualElement.Q<SliderInt>("year-slider").value);
        this.mainMenu.rootVisualElement.Q<SliderInt>("year-slider").RegisterValueChangedCallback((ev) => UpdateYear(ev.newValue));

        UpdateMonth(this.mainMenu.rootVisualElement.Q<SliderInt>("month-slider").value);
        this.mainMenu.rootVisualElement.Q<SliderInt>("month-slider").RegisterValueChangedCallback((ev) => UpdateMonth(ev.newValue));

        UpdateHour(this.mainMenu.rootVisualElement.Q<SliderInt>("hour-slider").value);
        this.mainMenu.rootVisualElement.Q<SliderInt>("hour-slider").RegisterValueChangedCallback((ev) => UpdateHour(ev.newValue));
    }

    public void HideAllMenus()
    {
        this.mainMenu.enabled = false;
        this.settingsMenu.enabled = false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    public void OnShowMenuClicked(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            this.ActivateMenu();
        }
    }

    private int GetResolutionIndex()
    {
        // Find current resolution index.
        // Note that Array.IndexOf with Screen.currentResolution does not work 
        // because Resolution structs don't match (refresh rate data is missing).
        int currentResolutionIndex = -1;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var resolution = Screen.resolutions[i];
            if (resolution.height == Screen.height && resolution.width == Screen.width)
            {
                currentResolutionIndex = i;
                break;
            }
        }

        return currentResolutionIndex;
    }

    void UpdateYear(int year)
    {
        this.timeMachine.year = year;
        this.sunPosition.year = year;
        this.mainMenu.rootVisualElement.Q<Label>("year-indicator").text = year.ToString();
    }

    void UpdateMonth(int month)
    {

        this.sunPosition.month = month;
        this.mainMenu.rootVisualElement.Q<Label>("month-indicator").text = monthNames[(int)month - 1];
    }

    void UpdateHour(int hour)
    {
        this.sunPosition.hour = hour;
        this.mainMenu.rootVisualElement.Q<Label>("hour-indicator").text = hour.ToString("00") + ":00";
    }

}