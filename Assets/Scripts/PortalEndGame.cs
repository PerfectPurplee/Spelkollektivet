using UnityEngine;

public class PortalEndGame : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent<Player.Player>(out Player.Player player)) {
            
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        }
    }

    private void OnEnable() {
        
    }
}