using System;
using UnityEngine;

public class Testing : MonoBehaviour {
    public static Testing Instance { get; private set; }


    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            XPManager.Instance.AddXP(3);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseMenuUI.Instance.Show();
        }

    }
}

