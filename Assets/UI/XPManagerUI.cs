using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class LevelManagerUI : MonoBehaviour {

    
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    
    private void Start() {
        XPManager.Instance.OnLevelUp += XPManager_OnLevelUp;
    }

    private void XPManager_OnLevelUp(object sender, EventArgs e) {
        levelNumberText.text = XPManager.Instance.GetLevelNumber().ToString();
    }

    private void Update() {
        expSlider.value = XPManager.Instance.GetCurrentXPNormalized();
    }
    
    




}