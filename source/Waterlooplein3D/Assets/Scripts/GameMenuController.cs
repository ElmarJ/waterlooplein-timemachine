using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        this.mainMenuCanvas.enabled = false;
        this.mainMenu.SetActive(false);

        this.settingsMenuCanvas.enabled = false;
        this.settingsMenu.SetActive(false);

        this.fpCharacter.enableCameraMovement = true;
        this.fpCharacter.lockAndHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        this.fpCharacter.playerCanMove = true;
        this.fpCharacter.playerCanMove = true;
    }

    public void ActivateMenu()
    {   
        this.mainMenuCanvas.enabled = true;
        this.mainMenu.SetActive(true);

        this.settingsMenuCanvas.enabled = false;
        this.settingsMenu.SetActive(false);

        this.fpCharacter.enableCameraMovement = false;
        this.fpCharacter.lockAndHideCursor = false;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        this.fpCharacter.playerCanMove = false;
        this.fpCharacter.playerCanMove = false;
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
