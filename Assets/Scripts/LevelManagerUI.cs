using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelManagerUI : MonoBehaviour {

    public event EventHandler OnLevelUp;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    
    private static int currentLevel = 1;
    private int xpNeededToLevelUp = currentLevel * 10;
    
    private void Start() {
        Testing.Instance.OnXPGained += Testing_OnXpGained;
    }
    
    private void Testing_OnXpGained(object sender, EventArgs e) {
        expSlider.value += 0.01f;
        if (expSlider.value >= 1) {
            OnLevelUp?.Invoke(this, EventArgs.Empty);
            // it should be in the event that's in different class managing levels but for now it's here
            currentLevel++;
            levelText.text = currentLevel.ToString();
            //
            
            
            
            expSlider.value = 0;
        }
    }
    
}