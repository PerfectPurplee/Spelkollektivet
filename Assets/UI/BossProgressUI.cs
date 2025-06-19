using System;
using UnityEngine;
using UnityEngine.UI;

public class BossProgressUI : MonoBehaviour {
    public static BossProgressUI Instance { get; private set; }

    [SerializeField] private Image gemImage1;
    [SerializeField] private Image gemImage2;
    [SerializeField] private Image gemImage3;
    [SerializeField] private Image gemImage4;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Instance of BossProgressUI");
        }

        Instance = this;

        HideAllGems();
    }

    private void Start() {
        GemCollectible.OnGemCollected += GemCollectible_OnGemCollected;
    }

    private void GemCollectible_OnGemCollected(object sender, EventArgs e) {
        switch (BossProgress.Instance.gemsCollected) {
            case 0: HideAllGems(); break;
            case 1: gemImage1.gameObject.SetActive(true); break;
            case 2: gemImage2.gameObject.SetActive(true); break;
            case 3: gemImage3.gameObject.SetActive(true); break;
            case 4: gemImage4.gameObject.SetActive(true); break;
        }
    }


    private void HideAllGems() {
        gemImage1.gameObject.SetActive(false);
        gemImage2.gameObject.SetActive(false);
        gemImage3.gameObject.SetActive(false);
        gemImage4.gameObject.SetActive(false);
    }
}