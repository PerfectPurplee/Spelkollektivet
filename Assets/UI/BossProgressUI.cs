using System;
using System.Collections.Generic;
using System.Linq;
using Interface;
using TMPro;
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

    [SerializeField] private List<Transform> collectiblesList;

    [SerializeField] private Transform arrowTransform;
    private Vector3 targetPosition;

    [SerializeField] private GameObject portal;

    [SerializeField] private GameObject bossHPBar;
    [SerializeField] public Slider bossHpSlider;
    [SerializeField] public TextMeshProUGUI bossHpText;
    
    private bool subscribed = false;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Instance of BossProgressUI");
        }
        Instance = this;
        
        
        HideAllGemsFlamesAndBossHP();
    }

    private void Start() {
        GemCollectible.OnGemCollected += GemCollectible_OnGemCollected;
        
    }

    private void GameBoss_OnDamageTaken(object sender, IDamageable.DamageTakenArgs e) {
        Debug.Log($"Damage taken: {e.CurrentHealth}");
        float bossHpNoralized = (float) e.CurrentHealth / Boss.GameBoss.Instance.MaxHealth;
        bossHpSlider.value = bossHpNoralized;
        bossHpText.text = e.CurrentHealth + "/" + Boss.GameBoss.Instance.MaxHealth;
    }

    private void Update() {
        if (BossProgress.Instance.gemsCollected < 4) {
            // Looking for nearest gem
            targetPosition = FindClosestGemTransform().position;
        }
        else {
            // All gems collected
            if (Boss.GameBoss.Instance != null) {
                // Targeting boss
                Boss.GameBoss.Instance.OnDamageTaken += GameBoss_OnDamageTaken;
                targetPosition = Boss.GameBoss.Instance.transform.position;
                bossHPBar.SetActive(true);
            }
            else {
                // Boss dead, targeting portal
                bossHPBar.SetActive(false);
                portal.SetActive(true);
                targetPosition = portal.transform.position;
            }
        }

        UpdateArrowRotation();
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


    private void UpdateArrowRotation() {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Player.Player.Instance.transform.position;
        fromPosition.y = 0f;
        Vector3 direction = (toPosition - fromPosition).normalized;
        float angle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        arrowTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void HideAllGemsFlamesAndBossHP() {
        gemImage1.gameObject.SetActive(false);
        gemImage2.gameObject.SetActive(false);
        gemImage3.gameObject.SetActive(false);
        gemImage4.gameObject.SetActive(false);
        altarFlame1.gameObject.SetActive(false);
        altarFlame2.gameObject.SetActive(false);
        altarFlame3.gameObject.SetActive(false);
        altarFlame4.gameObject.SetActive(false);
        bossHPBar.gameObject.SetActive(false);
    }


    private Transform FindClosestGemTransform() {
        Vector3 playerPosition = Player.Player.Instance.transform.position;

        collectiblesList.RemoveAll(item => item == null);

        return collectiblesList
            .OrderBy(gem => Vector3.Distance(gem.position, playerPosition))
            .FirstOrDefault();
    }
}