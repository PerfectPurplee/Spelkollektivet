using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    public static MainMenuUI Instance { get; private set; }
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    public event EventHandler OnButtonPressed;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of MainMenuUI");
        }
        Instance = this;
        
        playButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            Loader.LoadScene(Loader.Scene.Map);
        });
        settingsButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            Debug.Log("settings button clicked");
        });
        quitButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            Application.Quit();
        });
    }
}