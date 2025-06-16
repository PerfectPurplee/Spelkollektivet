using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
    
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    
    private int currentHealth;
    private int maxHealth = 1000;


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
        healthSlider.value = (float)currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
    }
}
