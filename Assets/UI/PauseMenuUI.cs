using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
    
    public static PauseMenuUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    public event EventHandler OnButtonPressed;
    public event EventHandler OnButtonHovered;
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of PauseMenuUI");
        }
        Instance = this;
        
        resumeButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            PauseManager.Instance.TogglePause();
        });
        settingsButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            Debug.Log("Settings");
        });
        mainMenuButton.onClick.AddListener(() => {
            OnButtonPressed?.Invoke(this, EventArgs.Empty);
            Hide();
            Time.timeScale = 1f;
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });

        //AddHoverTrigger(resumeButton);
        //AddHoverTrigger(mainMenuButton);
        //AddHoverTrigger(settingsButton);
        
        Hide();
    }


    private void AddHoverTrigger(Button button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
            Debug.Log("Czy log pomaga?");
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };

        entry.callback.AddListener((_) => InvokeOnButtonHovered());
        trigger.triggers.Add(entry);

    }
    public void InvokeOnButtonHovered()
    {
        OnButtonHovered?.Invoke(this, EventArgs.Empty);
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}