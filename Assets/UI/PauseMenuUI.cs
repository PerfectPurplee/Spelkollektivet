using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
    
    public static PauseMenuUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of PauseMenuUI");
        }
        Instance = this;
        
        resumeButton.onClick.AddListener(() => {
            Hide();
        });
        settingsButton.onClick.AddListener(() => {
            Debug.Log("Settings");
        });
        mainMenuButton.onClick.AddListener(() => {
            Hide();
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });
        
        Hide();
    }


    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}