using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    public FirstPersonDrifter firstPersonController;
    public GameMenuController gameMenuController;
    InputAction showMenuInputAction;

    void Start()
    {
        this.ShowMenu();
        showMenuInputAction = InputSystem.actions["ShowMenu"];
        showMenuInputAction.Enable();
        showMenuInputAction.performed += this.OnShowMenuInputActionPerformed;        
    }

    void OnEnable()
    {
    }

    public void StartGamePlay()
    {
        this.SetMenuMouseState(false);
        gameMenuController.HideAllMenus();
    }

    public void ShowMenu()
    {
        this.SetMenuMouseState(true);
        gameMenuController.ShowMainMenu();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void SetMenuMouseState(bool mouseHasMenuState)
    {
        UnityEngine.Cursor.lockState = mouseHasMenuState ? CursorLockMode.None : CursorLockMode.Locked;
        UnityEngine.Cursor.visible = mouseHasMenuState;
        if (mouseHasMenuState)
        {
            InputSystem.actions.FindActionMap("Player").Disable();
            InputSystem.actions.FindActionMap("Game").Disable();
            InputSystem.actions.FindActionMap("UI").Enable();
        }
        else
        {
            InputSystem.actions.FindActionMap("Player").Enable();
            InputSystem.actions.FindActionMap("Game").Enable();
            InputSystem.actions.FindActionMap("UI").Disable();
        }
        this.firstPersonController.enabled = !mouseHasMenuState;
    }

    private void OnShowMenuInputActionPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            this.ShowMenu();
        }
    }
}

