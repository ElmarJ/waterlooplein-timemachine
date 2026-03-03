using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Linq;

public class GameMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu; 
    public GameObject OSDControl;
    private readonly string[] monthNames = { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

    // TODO: consider using a state machine that keeps track of the current menu (or general state of the game)
    public void ShowSettingsMenu()
    {
        this.mainMenu.SetActive(false);
        this.settingsMenu.SetActive(true);
        this.OSDControl.SetActive(false);
    }

    public void ShowMainMenu()
    {
        this.mainMenu.SetActive(true);  
        this.settingsMenu.SetActive(false);
        this.OSDControl.SetActive(false);
    }

    public void HideAllMenus()
    {
        this.mainMenu.SetActive(false);
        this.settingsMenu.SetActive(false);
        this.OSDControl.SetActive(true);
    }
}