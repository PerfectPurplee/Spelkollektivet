using System;
using UnityEngine;

public class BossProgress : MonoBehaviour {
    public static BossProgress Instance { get; private set; }
    public event EventHandler OnGemCollected;
    public int gemsCollected;
    private int maxGems = 4;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Instance of BossProgress");
        }

        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.G) && gemsCollected < maxGems) {
            gemsCollected++;
            OnGemCollected?.Invoke(this, EventArgs.Empty);
        }
    }
}