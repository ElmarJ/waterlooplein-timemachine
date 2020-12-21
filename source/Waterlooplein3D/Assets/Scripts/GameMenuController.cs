using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameMenuController : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject settingsMenu;
    private Canvas mainMenuCanvas;
    private Canvas settingsMenuCanvas;
    private FirstPersonAIO fpCharacter;

    // Start is called before the first frame update
    void Start()
    {
        this.mainMenu = GameObject.Find("MainMenu");
        this.mainMenuCanvas = mainMenu?.GetComponent<Canvas>();
        this.settingsMenu = GameObject.Find("SettingsMenu");
        this.settingsMenuCanvas = this.settingsMenu?.GetComponent<Canvas>();
        this.fpCharacter = GameObject.Find("CharacterController").GetComponent<FirstPersonAIO>();
        
        this.mainMenuCanvas.enabled = true;
        this.settingsMenuCanvas.enabled = false;
        this.fpCharacter.controllerPauseState = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        this.mainMenuCanvas.enabled = false;
        this.mainMenu.SetActive(false);

        this.fpCharacter.lockAndHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        this.fpCharacter.controllerPauseState = false;

    }

    public void ActivateMenu()
    {   
        this.mainMenuCanvas.enabled = true;
        this.mainMenu.SetActive(true);

        this.fpCharacter.lockAndHideCursor = false;
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true;
        this.fpCharacter.controllerPauseState = true;
    }

    public void ShowSettingsMenu()
    {
        this.mainMenuCanvas.enabled = false;
        this.mainMenu.SetActive(false);

        this.settingsMenuCanvas.enabled = true;
        this.settingsMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        this.mainMenuCanvas.enabled = true;
        this.mainMenu.SetActive(true);

        this.settingsMenuCanvas.enabled = false;
        this.settingsMenu.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
