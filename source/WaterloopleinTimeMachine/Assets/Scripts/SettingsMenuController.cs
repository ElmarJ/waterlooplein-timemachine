using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SettingsMenuController : MonoBehaviour
{
    private UIDocument uidoc;
    public GameMenuController gameMenuController;

    void OnEnable()
    {
        this.uidoc = GetComponent<UIDocument>();
        this.uidoc.rootVisualElement.Q<Button>("back-button").clicked += gameMenuController.ShowMainMenu;

        var cancelAction = InputSystem.actions["Cancel"];
        cancelAction.Enable();
        cancelAction.performed += ev => gameMenuController.ShowMainMenu();

        var qualityDropdown = this.uidoc.rootVisualElement.Q<DropdownField>("quality-dropdown");
        qualityDropdown.choices = new(QualitySettings.names);
        qualityDropdown.index = QualitySettings.GetQualityLevel();
        qualityDropdown.RegisterValueChangedCallback(ev => QualitySettings.SetQualityLevel(qualityDropdown.index));

        var resolutionDropdown = this.uidoc.rootVisualElement.Q<DropdownField>("resolution-dropdown");
        resolutionDropdown.choices = (from resolution in Screen.resolutions select $"{resolution.width} x {resolution.height}").ToList();
        resolutionDropdown.index = GetResolutionIndex();
        resolutionDropdown.RegisterValueChangedCallback(ev => Screen.SetResolution(Screen.resolutions[resolutionDropdown.index].width, Screen.resolutions[resolutionDropdown.index].height, true));
    }

    void OnDisable()
    {
        if (this.uidoc.rootVisualElement != null)
        {
            this.uidoc.rootVisualElement.Q<Button>("back-button").clicked -= gameMenuController.ShowMainMenu;
        }
    
        var cancelAction = InputSystem.actions["Cancel"];
        cancelAction.performed -= ev => gameMenuController.ShowMainMenu();
        cancelAction.Disable();
    }

    private static int GetResolutionIndex()
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
