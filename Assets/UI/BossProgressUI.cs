using System;
using UnityEngine;
using UnityEngine.UI;

public class BossProgressUI : MonoBehaviour {
    public static BossProgressUI Instance { get; private set; }

    [SerializeField] private Image gemImage1;
    [SerializeField] private Image gemImage2;
    [SerializeField] private Image gemImage3;
    [SerializeField] private Image gemImage4;
    [SerializeField] private GameObject altarFlame1;
    [SerializeField] private GameObject altarFlame2;
    [SerializeField] private GameObject altarFlame3;
    [SerializeField] private GameObject altarFlame4;

    [SerializeField] private Transform arrowTransform;
    private Vector3 targetPosition = Vector3.zero;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Instance of BossProgressUI");
        }

        Instance = this;

        HideAllGemsAndFlames();
    }

    private void Start() {
        GemCollectible.OnGemCollected += GemCollectible_OnGemCollected;
    }

    private void Update() {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        arrowTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void GemCollectible_OnGemCollected(object sender, EventArgs e) {
        switch (BossProgress.Instance.gemsCollected) {
            case 1:
                gemImage1.gameObject.SetActive(true);
                altarFlame1.gameObject.SetActive(true);
                break;
            case 2:
                gemImage2.gameObject.SetActive(true);
                altarFlame2.gameObject.SetActive(true);
                break;
            case 3:
                gemImage3.gameObject.SetActive(true);
                altarFlame3.gameObject.SetActive(true);
                break;
            case 4:
                gemImage4.gameObject.SetActive(true);
                altarFlame4.gameObject.SetActive(true);
                break;
        }
    }


    private void HideAllGemsAndFlames() {
        gemImage1.gameObject.SetActive(false);
        gemImage2.gameObject.SetActive(false);
        gemImage3.gameObject.SetActive(false);
        gemImage4.gameObject.SetActive(false);
        altarFlame1.gameObject.SetActive(false);
        altarFlame2.gameObject.SetActive(false);
        altarFlame3.gameObject.SetActive(false);
        altarFlame4.gameObject.SetActive(false);
    }
}