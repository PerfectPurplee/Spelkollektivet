using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
    public class EnemySpawnManager : MonoBehaviour {
        [SerializeField] private GameObject meleeEnemyPrefab;
        [SerializeField] private GameObject rangedEnemyPrefab;

        [SerializeField] private float spawnRate;
        [SerializeField] private int enemiesPerWave;
        [SerializeField] private float spawnDistanceFromCamera = 15f;
        [SerializeField] private float distanceToTeleport;

        private float _timer;
        private Camera _mainCamera;

        private List<GameObject> enemiesSpawned = new List<GameObject>();

        void Start() {
            _mainCamera = Camera.main;
            InvokeRepeating(nameof(SpawnWave), 0f, spawnRate);
        }

        void FixedUpdate() 
        {
            Debug.Log("qbek82");
            enemiesSpawned.RemoveAll(enemy => enemy == null);
            foreach (GameObject enemy in enemiesSpawned)
            {
                if (enemy == null)
                {
                    continue;
                }
                Debug.Log("qbek1");
                if (Vector3.Distance(enemy.transform.position, Player.Player.Instance.transform.position) > distanceToTeleport)
                {
                    Debug.Log("qbek2");
                    enemy.transform.position = GetSpawnPositionOutsideCamera();
                }
            }
        }


        private void SpawnWave() {
            for (int i = 0; i < enemiesPerWave; i++) {
                Vector3 spawnPosition = GetSpawnPositionOutsideCamera();
                GameObject prefabToSpawn = Random.value < 0.5f ? meleeEnemyPrefab : rangedEnemyPrefab;
                enemiesSpawned.Add(Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity));
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