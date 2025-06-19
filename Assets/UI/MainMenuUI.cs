using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    public static MainMenuUI Instance { get; private set; }
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;

    public event EventHandler OnButtonPressed;
    public event EventHandler OnButtonHovered;

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

        AddHoverTrigger(quitButton);
        AddHoverTrigger(playButton);
        AddHoverTrigger(settingsButton);
    }

    private void AddHoverTrigger(Button button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
            Debug.Log("Nie ruszac tego loga! Bez niego nie dziala");
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };

        entry.callback.AddListener((_) => InvokeOnButtonHovered());
        trigger.triggers.Add(entry);

    }

    private void InvokeOnButtonHovered()
    {
        OnButtonHovered?.Invoke(this, EventArgs.Empty);
    }
}