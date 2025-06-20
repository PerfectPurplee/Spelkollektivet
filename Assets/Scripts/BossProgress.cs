using System;
using UnityEngine;

public class BossProgress : MonoBehaviour {
    public static BossProgress Instance { get; private set; }
    public int gemsCollected;
    private int maxGems = 4;

    [SerializeField] private Boss.GameBoss boss;


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
        boss.gameObject.SetActive(true);
        BossProgressUI.Instance.bossHpSlider.value = 1f;
        BossProgressUI.Instance.bossHpText.text = Boss.GameBoss.Instance.CurrentHealth + "/" + Boss.GameBoss.Instance.MaxHealth;
        Debug.Log("Spawning boss xdd");
    }
}