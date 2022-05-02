using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject player;
    public Button selectedButtonMainMenu;
    public Button selectedButtonSettingsMenu;

    private PlayerInput playerInput;
    private Canvas mainMenuCanvas;
    private Canvas settingsMenuCanvas;

    void Start()
    {
        this.playerInput = player.GetComponent<PlayerInput>();

        mainMenuCanvas = mainMenu.GetComponent<Canvas>();
        settingsMenuCanvas = settingsMenu.GetComponent<Canvas>();

        this.ActivateMenu();        
        this.ShowMainMenu();
    }

    public void ResumeGame()
    {
        this.mainMenuCanvas.enabled = false;
        this.mainMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        this.playerInput.actions.Enable();
        this.player.GetComponent<FirstPersonDrifter>().enabled = true;

    }

    public void ActivateMenu()
    {   
        this.mainMenuCanvas.enabled = true;
        this.mainMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        this.playerInput.actions.Disable();
        this.player.GetComponent<FirstPersonDrifter>().enabled = false;
    }

    public void ShowSettingsMenu()
    {
        this.mainMenuCanvas.enabled = false;
        this.mainMenu.SetActive(false);

        this.settingsMenuCanvas.enabled = true;
        this.settingsMenu.SetActive(true);

        selectedButtonSettingsMenu.Select();
    }

    public void ShowMainMenu()
    {
        this.mainMenuCanvas.enabled = true;
        this.mainMenu.SetActive(true);

        this.settingsMenuCanvas.enabled = false;
        this.settingsMenu.SetActive(false);

        selectedButtonMainMenu.Select();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OnShowMenuClicked(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            this.ActivateMenu();
        }
    }
}