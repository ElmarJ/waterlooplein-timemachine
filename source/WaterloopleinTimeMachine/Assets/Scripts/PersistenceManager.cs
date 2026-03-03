using GeoJsonCityBuilder;
using GeoJsonCityBuilder.Components;
using UnityEngine;

// Persist settings (quality level and resolution) from PlayerPrefs when the game starts
public class PersistenceManager : MonoBehaviour
{
    public GameObject character;
    public TimeController timeController;

    void Start()
    {
        // Load fullscreen
        if (PlayerPrefs.HasKey("fullscreen"))
        {
            Screen.fullScreen = PlayerPrefs.GetInt("fullscreen") == 1;
        }

        // Load quality level
        if (PlayerPrefs.HasKey("qualityLevel"))
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityLevel"));
        }

        // Load resolution
        if (PlayerPrefs.HasKey("resolutionWidth") && PlayerPrefs.HasKey("resolutionHeight"))
        {
            Screen.SetResolution(PlayerPrefs.GetInt("resolutionWidth"), PlayerPrefs.GetInt("resolutionHeight"), Screen.fullScreen);
        }

        // Load character position
        if (PlayerPrefs.HasKey("characterPositionX") && PlayerPrefs.HasKey("characterPositionY") && PlayerPrefs.HasKey("characterPositionZ"))
        {
            character.transform.position = new Vector3(PlayerPrefs.GetFloat("characterPositionX"), PlayerPrefs.GetFloat("characterPositionY"), PlayerPrefs.GetFloat("characterPositionZ"));
        }

        // Load character rotation
        if (PlayerPrefs.HasKey("characterRotationY"))
        {
            character.transform.rotation = Quaternion.Euler(0, PlayerPrefs.GetFloat("characterRotationY"), 0);
        }

        // Load date and time
        if (PlayerPrefs.HasKey("year"))
        {
            timeController.year = PlayerPrefs.GetInt("year");
        }

        if (PlayerPrefs.HasKey("month"))
        {
            timeController.month = PlayerPrefs.GetInt("month");
        }

        if (PlayerPrefs.HasKey("day"))
        {
            timeController.day = PlayerPrefs.GetInt("day");
        }

        if (PlayerPrefs.HasKey("hour"))
        {
            timeController.hour = PlayerPrefs.GetInt("hour");
        }
    }

    // Save settings when the game is closed

    void OnApplicationQuit()
    {
        // Save fullscreen
        PlayerPrefs.SetInt("fullscreen", Screen.fullScreen ? 1 : 0);

        // Save quality level
        PlayerPrefs.SetInt("qualityLevel", QualitySettings.GetQualityLevel());

        // Save resolution
        PlayerPrefs.SetInt("resolutionWidth", Screen.currentResolution.width);
        PlayerPrefs.SetInt("resolutionHeight", Screen.currentResolution.height);

        // Save character position
        PlayerPrefs.SetFloat("characterPositionX", character.transform.position.x);
        PlayerPrefs.SetFloat("characterPositionY", character.transform.position.y);
        PlayerPrefs.SetFloat("characterPositionZ", character.transform.position.z);
        PlayerPrefs.SetFloat("characterRotationY", character.transform.rotation.eulerAngles.y);

        // Save date and time
        PlayerPrefs.SetInt("year", timeController.year);
        PlayerPrefs.SetInt("month", timeController.month);
        PlayerPrefs.SetInt("day", timeController.day);
        PlayerPrefs.SetInt("hour", timeController.hour);
    }
}
