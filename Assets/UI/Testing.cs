using System;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour {
    public static Testing Instance { get; private set; }
    
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            XPManager.Instance.GainExp(3);
        }

    }
}

