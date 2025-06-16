using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
    
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    
    private int currentHealth;
    private int maxHealth = 10;


    private void Start() {
        Testing.Instance.OnDamageTaken += TestingOnDamageTaken;
        Testing.Instance.OnHealTaken += TestingOnHealTaken;
        currentHealth = maxHealth;
    }

    private void TestingOnHealTaken(object sender, Testing.OnHealTakenEventArgs e) {
        currentHealth = Mathf.Min(maxHealth, currentHealth + e.heal);
        healthSlider.value = (float)currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    private void TestingOnDamageTaken(object sender, Testing.OnDamageTakenEventArgs e) {
        currentHealth = Mathf.Max(0, currentHealth - e.damage);
        healthSlider.value = (float)currentHealth / maxHealth;
        healthText.text = currentHealth + "/" + maxHealth;
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
