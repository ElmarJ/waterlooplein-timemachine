using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(PanelRenderer))]
public class SettingsMenuController : MonoBehaviour
{
    private PanelRenderer panelRenderer;
    private Button backButton;
    private DropdownField qualityDropdown;
    private DropdownField resolutionDropdown;
    private InputAction cancelAction;
    public GameMenuController gameMenuController;

    void OnEnable()
    {
        this.panelRenderer = GetComponent<PanelRenderer>();
        this.panelRenderer.RegisterUIReloadCallback(OnUIReload);

        this.cancelAction = InputSystem.actions["Cancel"];
        this.cancelAction.Enable();
        this.cancelAction.performed += OnCancelPerformed;

        BindUI(this.panelRenderer.rootVisualElement);
    }

    void OnDisable()
    {
        if (this.panelRenderer != null)
        {
            UnbindUI();
            this.panelRenderer.UnregisterUIReloadCallback(OnUIReload);
        }

        if (this.cancelAction != null)
        {
            this.cancelAction.performed -= OnCancelPerformed;
            this.cancelAction.Disable();
            this.cancelAction = null;
        }
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

    private void OnUIReload(PanelRenderer renderer, VisualElement rootElement)
    {
        UnbindUI();
        BindUI(rootElement);
    }

    private void BindUI(VisualElement rootElement)
    {
        if (rootElement == null)
        {
            return;
        }

        UnbindUI();

        this.backButton = rootElement.Q<Button>("back-button");
        this.backButton.clicked += gameMenuController.ShowMainMenu;

        this.qualityDropdown = rootElement.Q<DropdownField>("quality-dropdown");
        this.qualityDropdown.choices = new(QualitySettings.names);
        this.qualityDropdown.index = QualitySettings.GetQualityLevel();
        this.qualityDropdown.RegisterValueChangedCallback(OnQualityChanged);

        this.resolutionDropdown = rootElement.Q<DropdownField>("resolution-dropdown");
        this.resolutionDropdown.choices = (from resolution in Screen.resolutions select $"{resolution.width} x {resolution.height}").ToList();
        this.resolutionDropdown.index = GetResolutionIndex();
        this.resolutionDropdown.RegisterValueChangedCallback(OnResolutionChanged);
    }

    private void UnbindUI()
    {
        if (this.backButton != null)
        {
            this.backButton.clicked -= gameMenuController.ShowMainMenu;
            this.backButton = null;
        }

        if (this.qualityDropdown != null)
        {
            this.qualityDropdown.UnregisterValueChangedCallback(OnQualityChanged);
            this.qualityDropdown = null;
        }

        if (this.resolutionDropdown != null)
        {
            this.resolutionDropdown.UnregisterValueChangedCallback(OnResolutionChanged);
            this.resolutionDropdown = null;
        }
    }

    private void OnCancelPerformed(InputAction.CallbackContext context)
    {
        gameMenuController.ShowMainMenu();
    }

    private void OnQualityChanged(ChangeEvent<string> changeEvent)
    {
        QualitySettings.SetQualityLevel(this.qualityDropdown.index);
    }

    private void OnResolutionChanged(ChangeEvent<string> changeEvent)
    {
        Screen.SetResolution(Screen.resolutions[this.resolutionDropdown.index].width, Screen.resolutions[this.resolutionDropdown.index].height, true);
    }
}
