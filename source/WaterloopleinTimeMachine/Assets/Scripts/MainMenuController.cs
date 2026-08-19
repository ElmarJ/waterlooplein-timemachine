using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PanelRenderer))]
public class MainMenuController : MonoBehaviour
{
    private PanelRenderer panelRenderer;
    private Button resumeButton;
    private Button settingsButton;
    private Button exitButton;
    public TimeController timeController;
    public GameManager gameManager;
    public GameMenuController gameMenuController;

    void OnEnable()
    {
        // Note: for some reason, ClickEvent callbacks registered with RegisterCallback are not triggered when the button is pressed
        //     through something else than a mouse click (e.g. gamepad or keyboard). Using the clicked event instead seems to work 
        //     for all input types.

        this.panelRenderer = GetComponent<PanelRenderer>();
        this.panelRenderer.RegisterUIReloadCallback(OnUIReload);
        BindUI(this.panelRenderer.rootVisualElement);
    }

    void OnDisable()
    {
        if (this.panelRenderer != null)
        {
            UnbindUI();
            this.panelRenderer.UnregisterUIReloadCallback(OnUIReload);
        }
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

        this.resumeButton = rootElement.Q<Button>("resume-button");
        this.settingsButton = rootElement.Q<Button>("settings-button");
        this.exitButton = rootElement.Q<Button>("exit-button");

        this.resumeButton.clicked += gameManager.StartGamePlay;
        this.settingsButton.clicked += gameMenuController.ShowSettingsMenu;
        this.exitButton.clicked += gameManager.ExitGame;

        // Set the data context for the time controls to the Time Machine controller
        rootElement.Q<VisualElement>("time-controls").dataSource = this.timeController;
        this.resumeButton.Focus();
    }

    private void UnbindUI()
    {
        if (this.resumeButton != null)
        {
            this.resumeButton.clicked -= gameManager.StartGamePlay;
            this.resumeButton = null;
        }

        if (this.settingsButton != null)
        {
            this.settingsButton.clicked -= gameMenuController.ShowSettingsMenu;
            this.settingsButton = null;
        }

        if (this.exitButton != null)
        {
            this.exitButton.clicked -= gameManager.ExitGame;
            this.exitButton = null;
        }
    }
}
