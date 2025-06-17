using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private int currentHealth;
    private int maxHealth = 10;


    private void Start() {
        Testing.Instance.OnDamageTaken += Testing_OnDamageTaken;
        Testing.Instance.OnHealTaken += Testing_OnHealTaken;
        currentHealth = maxHealth;


        UpdateVisual();
    }

    private void Testing_OnHealTaken(object sender, Testing.OnHealTakenEventArgs e) {
        currentHealth = Mathf.Min(maxHealth, currentHealth + e.healAmount);

        UpdateVisual();
    }

    private void Testing_OnDamageTaken(object sender, Testing.OnDamageTakenEventArgs e) {
        currentHealth = Mathf.Max(0, currentHealth - e.damageAmount);

        UpdateVisual();
    }

    private void UpdateVisual() {
        float healthVisualNormalized = (float)currentHealth / maxHealth;
        if (healthVisualNormalized > 0.8f && healthVisualNormalized < 1f) {
            healthSlider.value = 0.8f;
        }
        else if (healthVisualNormalized < 0.2f && healthVisualNormalized > 0f) {
            healthSlider.value = 0.2f;
        }
        else {
            healthSlider.value = healthVisualNormalized;
        }

        healthText.text = currentHealth + "/" + maxHealth;
    }
}