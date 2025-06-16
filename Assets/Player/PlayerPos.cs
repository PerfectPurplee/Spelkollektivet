using UnityEngine;

namespace Player
{
    public class PlayerPos : MonoBehaviour
    {
        public static PlayerPos Instance { get; private set; }
        private void Awake()
        {
            if(Instance == null) Instance = this;
            else Destroy(gameObject);
            
        }
    }
}
