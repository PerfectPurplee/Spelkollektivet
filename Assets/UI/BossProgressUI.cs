using UnityEngine;
using UnityEngine.UI;

public class BossProgressUI : MonoBehaviour {
    
    public static BossProgressUI Instance {get; private set;}
    
    [SerializeField] private Image gemImage1;
    [SerializeField] private Image gemImage2;
    [SerializeField] private Image gemImage3;
    [SerializeField] private Image gemImage4;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one instance of BossProgressUI");
        }
        Instance = this;
        
        HideAllGems();
    }

    private void Start() {
        //Subscribe to events OnGemCollected or whatever
        
    }




    private void HideAllGems() {
        gemImage1.gameObject.SetActive(false);
        gemImage2.gameObject.SetActive(false);
        gemImage3.gameObject.SetActive(false);
        gemImage4.gameObject.SetActive(false);
    }
}