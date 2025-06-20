using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GemCollectible : MonoBehaviour {
    
    public static event EventHandler OnGemCollected;
    
    
    
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Player.Player>(out Player.Player player)) {
            
            OnGemCollected?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }
    }
}