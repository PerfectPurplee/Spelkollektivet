using System;
using UnityEngine;

public class BossProgress : MonoBehaviour {
    public static BossProgress Instance { get; private set; }
    public int gemsCollected;
    private int maxGems = 4;

    
    
    
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Instance of BossProgress");
        }

        Instance = this;
    }

    private void Start() {
        GemCollectible.OnGemCollected += GemCollectible_OnGemCollected;
    }

    private void GemCollectible_OnGemCollected(object sender, EventArgs e) {
        gemsCollected++;
        Debug.Log(gemsCollected);
        if (gemsCollected == maxGems) {
            SpawnBoss();
        }
    }


    private void SpawnBoss() {
        Debug.Log("Spawning boss xdd");
    }
}