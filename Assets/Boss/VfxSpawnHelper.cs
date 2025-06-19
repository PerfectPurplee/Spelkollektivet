using UnityEngine;

namespace Boss {
    public class VfxSpawnHelper : MonoBehaviour
    {
        public GameObject vfx;


        private void SpawnVFX() {
            GameObject.Instantiate(vfx, Boss.GameBoss.Instance.transform.position, transform.rotation);
        }

    }
}
