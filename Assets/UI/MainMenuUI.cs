using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            Debug.Log("play button clicked");
        });
        settingsButton.onClick.AddListener(() => {
            Debug.Log("settings button clicked");
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}