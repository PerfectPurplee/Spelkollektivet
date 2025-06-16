using System;
using UnityEngine;

public class Testing : MonoBehaviour {
    public static Testing Instance { get; private set; }
    public event EventHandler OnXPPickedUp;
    public event EventHandler<OnHealTakenEventArgs> OnHealTaken;

    public class OnHealTakenEventArgs : EventArgs {
        public int heal;
    }
    public event EventHandler<OnDamageTakenEventArgs> OnDamageTaken;

    public class OnDamageTakenEventArgs : EventArgs {
        public int damage;
    }
    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            OnXPPickedUp?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            OnDamageTaken?.Invoke(this, new OnDamageTakenEventArgs {
                damage = 1,
            });
        }
        
        if (Input.GetKeyDown(KeyCode.H)) {
            OnHealTaken.Invoke(this, new OnHealTakenEventArgs {
                heal = 1,
            });
        }

    }
}

