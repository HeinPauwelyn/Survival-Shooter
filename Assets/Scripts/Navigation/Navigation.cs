using UnityEngine;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Collections;

public class Navigation : MonoBehaviour
{
    public GameObject buttonPanel;
    public GameObject LevelPanel;

    private GameObject activePanel;
    
    public void Click(string commando)
    {
        switch (commando.ToLower())
        {
            case "show_levels":
                LevelPanel.SetActive(true);
                buttonPanel.SetActive(false);
                activePanel = LevelPanel;
                break;

            case "level01":
                Application.LoadLevel("Level 01");
                break;

            case "back":
                activePanel.SetActive(false);
                buttonPanel.SetActive(true);
                break;

            case "again":
                Application.LoadLevel(Application.loadedLevel);
                break;

            case "go_to_menu":
                Application.LoadLevel("Navigation");
                break;

            default:
                break;
        }
    }
}
