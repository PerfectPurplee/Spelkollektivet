using UnityEngine;

namespace Enemies {
    public class EnemySpawnManager : MonoBehaviour {
        [SerializeField] private GameObject meleeEnemyPrefab;
        [SerializeField] private GameObject rangedEnemyPrefab;

        [SerializeField] private float spawnRate;
        [SerializeField] private int enemiesPerWave;
        [SerializeField] private float spawnDistanceFromCamera = 15f;


        private float _timer;
        private Camera _mainCamera;


        void Start() {
            _mainCamera = Camera.main;
            InvokeRepeating(nameof(SpawnWave), 0f, spawnRate);
        }

        // Update is called once per frame
        void Update() {
        }


        private void SpawnWave() {
            for (int i = 0; i < enemiesPerWave; i++) {
                Vector3 spawnPosition = GetSpawnPositionOutsideCamera();
                GameObject prefabToSpawn = Random.value < 0.5f ? meleeEnemyPrefab : rangedEnemyPrefab;
                Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }
        }

        private Vector3 GetSpawnPositionOutsideCamera() {
            Vector3[] directions = {
                Vector3.forward, Vector3.back,
                Vector3.left, Vector3.right
            };

            Vector3 dir = directions[Random.Range(0, directions.Length)];

            Vector3 camPos = _mainCamera.transform.position;
            Vector3 spawnPos = camPos + dir * spawnDistanceFromCamera;
            spawnPos.y = 0f;

            return spawnPos;
        }
    }
}