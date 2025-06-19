using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManagerGame : MonoBehaviour {
    public static SoundManagerGame Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    // private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    private float volume;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of SoundManager");
        }

        Instance = this;

        // volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
        volume = 4f;
    }

    private void Start() {
        PauseMenuUI.Instance.OnButtonPressed += PauseMenuUI_OnButtonPressed;
        XPManager.Instance.OnLevelUp += XPManager_OnLevelUp;
        PauseMenuUI.Instance.OnButtonHovered += PauseMenuUI_OnButtonHovered;

    }

    private void XPManager_OnLevelUp(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.levelUp, Camera.main.transform.position);
    }

    private void PauseMenuUI_OnButtonPressed(object sender, EventArgs e) {
        PlaySound(audioClipRefsSO.menuOption, Camera.main.transform.position);
    }
    private void PauseMenuUI_OnButtonHovered(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSO.menuCategory, Camera.main.transform.position);
        Debug.Log("Button is hovered in the Pause Menu");
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

    public void PlaySwordSwishSound()
    {
        PlaySound(audioClipRefsSO.swordHit, transform.position);
    }
}