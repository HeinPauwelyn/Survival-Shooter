using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameNavigation : MonoBehaviour
{
    public Button btnAgain;
    public Button btnMenu;

    public void Start()
    {
        btnAgain.onClick.AddListener(() => {
            Application.LoadLevel(Application.loadedLevel);
        });

        btnMenu.onClick.AddListener(() => {
            Application.LoadLevel("Navigation");
        });
    }
}
