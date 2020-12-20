using UnityEngine;
using System.Collections;
 
// Quits the player when the user hits escape
 
public class GameKeyController : MonoBehaviour
{
    private GameMenuController gameMenu;
    void Start()
    {
        this.gameMenu = GameObject.Find("MainMenu").GetComponent<GameMenuController>();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            // Application.Quit();
            gameMenu.ActivateMenu();
        }
    }
}