using System;
using UnityEngine;

public class XPManager : MonoBehaviour {
    public static XPManager Instance { get; private set; }


    public class ExpGainEventArgs : EventArgs {
        public int ExpAmount;

        public ExpGainEventArgs(int expAmount) {
            ExpAmount = expAmount;
        }
    }

    public event EventHandler<ExpGainEventArgs> OnExpGain;
    public event EventHandler OnLevelUp;

    private int levelNumber;
    private int currentXP;
    private int XPNeededToNextLevel;
    
    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of XPManager");
        }

        Instance = this;

        levelNumber = 1;
        currentXP = 0;
        XPNeededToNextLevel = 10;
    }

    public void GainExp(int xpAmount) {
        currentXP += xpAmount;
        OnExpGain?.Invoke(this, new ExpGainEventArgs(xpAmount));

        while (currentXP >= XPNeededToNextLevel) {
            levelNumber++;
            currentXP -= XPNeededToNextLevel;
            XPNeededToNextLevel = CountXPNeededToNextLevel();
            OnLevelUp?.Invoke(this, EventArgs.Empty);
        }

        Debug.Log("Level: " + levelNumber);
        Debug.Log("XP: " + currentXP);
        Debug.Log("XP Needed: " + XPNeededToNextLevel);
    }

    private int CountXPNeededToNextLevel() {
        return levelNumber * 10;
    }

    public int GetLevelNumber() {
        return levelNumber;
    }


    public float GetCurrentXPNormalized() {
        return (float)currentXP / XPNeededToNextLevel;
    }
}