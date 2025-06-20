using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour {
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    
    private void Start() {
        Player.Player.Instance.OnDamageTaken += Player_OnDamageTaken;
        Player.Player.Instance.OnHealthRegen += Player_OnHealthRegen;
        
        UpdateVisual();
    }

    private void Player_OnHealthRegen() {
        UpdateVisual();
    }

    private void Player_OnDamageTaken(object sender, IDamageable.DamageTakenArgs e) {
        UpdateVisual();
    }
    

    private void UpdateVisual() {
        float healthVisualNormalized = (float)Player.Player.Instance.CurrentHealth / Player.Player.Instance.MaxHealth;
        
        healthSlider.value = healthVisualNormalized;
        healthText.text = Player.Player.Instance.CurrentHealth + "/" + Player.Player.Instance.MaxHealth;
    }
}