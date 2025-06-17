using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {
    
    public static SoundManager Instance { get; private set; }
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
        PauseMenuUI.Instance.OnButtonPressed += PauseMenuUI_OnButtonPressed;
        XPManager.Instance.OnLevelUp += XPManager_OnLevelUp;
    }

    private void XPManager_OnLevelUp(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.levelUp, Camera.main.transform.position, 50f);
    }

    private void PauseMenuUI_OnButtonPressed(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.menuOption, Vector3.zero);
    }

    private void MainMenuUI_OnButtonPressed(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.menuCategory, Vector3.zero);
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * volume);
    }
    


}