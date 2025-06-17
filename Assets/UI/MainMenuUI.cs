using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.UIScene);
        });
        settingsButton.onClick.AddListener(() => {
            Debug.Log("settings button clicked");
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}