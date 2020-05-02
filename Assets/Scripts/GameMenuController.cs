using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    private Canvas canvas;
    private FirstPersonAIO fpCharacter;

    // Start is called before the first frame update
    void Start()
    {
        this.canvas = gameObject.GetComponent<Canvas>();
        this.fpCharacter = GameObject.Find("CharacterController").GetComponent<FirstPersonAIO>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResumeGame()
    {
        this.canvas.enabled = false;
        this.fpCharacter.enableCameraMovement = true;
        this.fpCharacter.lockAndHideCursor = true;
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        this.fpCharacter.playerCanMove = true;
        this.fpCharacter.playerCanMove = true;
    }

    public void ActivateMenu()
    {   
        this.canvas.enabled = true;
        this.fpCharacter.enableCameraMovement = false;
        this.fpCharacter.lockAndHideCursor = false;
        Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        this.fpCharacter.playerCanMove = false;
        this.fpCharacter.playerCanMove = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
