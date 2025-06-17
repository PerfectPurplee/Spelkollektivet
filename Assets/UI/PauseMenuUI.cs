using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
    
    public static PauseMenuUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    public event EventHandler OnButtonPressed;
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of PauseMenuUI");
        }
        Instance = this;
        
        resumeButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            Hide();
        });
        settingsButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            Debug.Log("Settings");
        });
        mainMenuButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
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