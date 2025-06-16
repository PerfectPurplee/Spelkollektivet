using System;
using UnityEngine;

public class Testing : MonoBehaviour {
    public static Testing Instance { get; private set; }
    public event EventHandler<OnXPGainedEventArgs> OnXPGained;

    public class OnXPGainedEventArgs : EventArgs {
    public int xpAmount;
}
    public event EventHandler<OnHealTakenEventArgs> OnHealTaken;

    public class OnHealTakenEventArgs : EventArgs {
        public int healAmount;
    }
    public event EventHandler<OnDamageTakenEventArgs> OnDamageTaken;

    public class OnDamageTakenEventArgs : EventArgs {
        public int damageAmount;
    }
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnXPGained?.Invoke(this, new OnXPGainedEventArgs {
                xpAmount = 1,
            });
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            OnDamageTaken?.Invoke(this, new OnDamageTakenEventArgs {
                damageAmount = 1,
            });
        }
        
        if (Input.GetKeyDown(KeyCode.H)) {
            OnHealTaken.Invoke(this, new OnHealTakenEventArgs {
                healAmount = 1,
            });
        }

    }
}

