using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenuController : MonoBehaviour
{
    private UIDocument uidoc;
    public TimeController timeController;
    public GameManager gameManager;
    public GameMenuController gameMenuController;

    void OnEnable()
    {
        // Note: for some reason, ClickEvent callbacks registered with RegisterCallback are not triggered when the button is pressed
        //     through something else than a mouse click (e.g. gamepad or keyboard). Using the clicked event instead seems to work 
        //     for all input types.

        this.uidoc = GetComponent<UIDocument>();
        this.uidoc.rootVisualElement.Q<Button>("resume-button").clicked += gameManager.StartGamePlay;
        this.uidoc.rootVisualElement.Q<Button>("settings-button").clicked += gameMenuController.ShowSettingsMenu;
        this.uidoc.rootVisualElement.Q<Button>("exit-button").clicked += gameManager.ExitGame;

        // Set the data context for the time controls to the Time Machine controller
        this.uidoc.rootVisualElement.Q<VisualElement>("time-controls").dataSource = this.timeController;
        this.uidoc.rootVisualElement.Q<Button>("resume-button").Focus();
    }

    void OnDisable()
    {
        if (this.uidoc.rootVisualElement != null)
        {
            this.uidoc.rootVisualElement.Q<Button>("resume-button").clicked -= gameManager.StartGamePlay;
            this.uidoc.rootVisualElement.Q<Button>("settings-button").clicked -= gameMenuController.ShowSettingsMenu;
            this.uidoc.rootVisualElement.Q<Button>("exit-button").clicked -= gameManager.ExitGame;
        }
    }
}
