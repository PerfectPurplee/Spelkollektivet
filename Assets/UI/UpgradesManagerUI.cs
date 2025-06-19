using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UpgradesManagerUI : MonoBehaviour {
    [SerializeField] private Button choiceAButton;
    [SerializeField] private Button choiceBButton;
    [SerializeField] private Button choiceCButton;

    public static UpgradesManagerUI Instance;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance UpgradesManagerUI");
        }

        Instance = this;

        choiceAButton.onClick.AddListener(() => {
            UpgradesManager.Instance.ChooseUpgradeByIndex(0);
            DestroyIconGameObjects();
            Hide();
        });
        choiceBButton.onClick.AddListener(() => {
            UpgradesManager.Instance.ChooseUpgradeByIndex(1);
            DestroyIconGameObjects();
            Hide();
        });
        choiceCButton.onClick.AddListener(() => {
            UpgradesManager.Instance.ChooseUpgradeByIndex(2);
            DestroyIconGameObjects();
            Hide();
        });
    }

    private void DestroyIconGameObjects() {
        foreach (Transform transform in UpgradesManager.Instance.iconsParents) {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    private void Start() {
        UpgradesManager.Instance.OnStartUpgradeChoice += UpgradesManager_OnStartUpgradeChoice;
        
        Hide();
    }


    private void UpgradesManager_OnStartUpgradeChoice(List<GameObject> obj) {
        Show();
    }

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}