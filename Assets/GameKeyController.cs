using UnityEngine;
using System.Collections;
 
// Quits the player when the user hits escape
 
public class GameKeyController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}