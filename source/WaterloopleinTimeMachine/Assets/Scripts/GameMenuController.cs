using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using System.Linq;

public class GameMenuController : MonoBehaviour
{
    public GameObject player;
    public UIDocument mainMenu;
    public UIDocument settingsMenu;
    public UIDocument OSDControl;
    public TimeController timeController;
    private InputAction showMenuAction;
    private readonly string[] monthNames = { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

    public void Start()
    {
        this.showMenuAction = InputSystem.actions["ShowMenu"];
        this.showMenuAction.Enable();
        this.showMenuAction.performed += this.OnShowMenuClicked;
    }

    public void OnEnable()
    {
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
        if (mouseHasMenuState)
        {
            InputSystem.actions.FindActionMap("Player").Disable();
            InputSystem.actions.FindActionMap("Game").Disable();
        }
        else
        {
            InputSystem.actions.FindActionMap("Player").Enable();
            InputSystem.actions.FindActionMap("Game").Enable();
        }
        this.player.GetComponent<FirstPersonDrifter>().enabled = !mouseHasMenuState;
    }

    public void ShowSettingsMenu()
    {
        this.mainMenu.enabled = false;
        this.settingsMenu.enabled = true;
        this.OSDControl.enabled = false;

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
        this.OSDControl.enabled = false;

        this.mainMenu.rootVisualElement.Q<Button>("resume-button").RegisterCallback<ClickEvent>(ev => this.ResumeGame());
        this.mainMenu.rootVisualElement.Q<Button>("settings-button").RegisterCallback<ClickEvent>(ev => this.ShowSettingsMenu());
        this.mainMenu.rootVisualElement.Q<Button>("exit-button").RegisterCallback<ClickEvent>(ev => this.ExitGame());

        // Set the data context for the time controls to the Time Machine controller
        this.mainMenu.rootVisualElement.Q<VisualElement>("time-controls").dataSource = this.timeController;

        this.mainMenu.rootVisualElement.Q<Button>("resume-button").Focus();
    }

    public void HideAllMenus()
    {
        this.mainMenu.enabled = false;
        this.settingsMenu.enabled = false;
        this.OSDControl.enabled = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
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



}