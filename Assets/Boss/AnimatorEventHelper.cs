using UnityEngine;

namespace Boss {
    public class AnimatorEventHelper : MonoBehaviour {
        public void OnAnimationAttackFinished() {
            Destroy(gameObject);
        }
    }
}