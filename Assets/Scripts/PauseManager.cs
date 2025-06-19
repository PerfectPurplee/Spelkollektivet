using UnityEngine;

public class PauseManager : MonoBehaviour {
    public static PauseManager Instance { get; private set; }
    private bool isPaused;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of PauseManager");
        }

        Instance = this;
        isPaused = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;

        if (isPaused) {
            Time.timeScale = 0f;
            PauseMenuUI.Instance.Show();
        }
        else {
            Time.timeScale = 1f;
            PauseMenuUI.Instance.Hide();
        }
    }
}