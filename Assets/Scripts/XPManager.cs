
using System;
using UnityEngine;

public class XPManager : MonoBehaviour {

    public static XPManager Instance { get; private set; }
    public event EventHandler<OnLevelUpEventArgs> OnLevelUp;

    public class OnLevelUpEventArgs : EventArgs {
        public int level;
    }
    private int levelNumber;
    private int currentXP;
    private int XPNeededToNextLevel;


    private void Awake() {
        Instance = this;

        levelNumber = 1;
        currentXP = 0;
        XPNeededToNextLevel = 10;
    }

    public void AddXP(int XPAmount) {
        currentXP += XPAmount;
        while (currentXP >= XPNeededToNextLevel) {
            levelNumber++;
            currentXP -= XPNeededToNextLevel;
            OnLevelUp?.Invoke(this, new OnLevelUpEventArgs {
                level = levelNumber,
            });
        }
    }

    public float GetCurrentXPNormalized() {
        return (float)currentXP / XPNeededToNextLevel;
    }
}