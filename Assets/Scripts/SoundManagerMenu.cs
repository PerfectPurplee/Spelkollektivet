using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManagerMenu : MonoBehaviour {
    public static SoundManagerMenu Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    // private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private float volume;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of SoundManager");
        }

        Instance = this;
        
        // volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
        volume = 1f;
    }

    private void Start() {
        MainMenuUI.Instance.OnButtonPressed += MainMenuUI_OnButtonPressed;
        MainMenuUI.Instance.OnButtonHovered += MainMenuUI_OnButtonHovered;
    }


    private void MainMenuUI_OnButtonPressed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.menuOption, Vector3.zero);
    }
    private void MainMenuUI_OnButtonHovered(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.menuCategory, Vector3.zero);
        Debug.Log("Button is hovered in the MainMenu");
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * volume);
    }

    public AudioClipRefsSO GetAudio()
    {
        return audioClipRefsSO;
    }
}