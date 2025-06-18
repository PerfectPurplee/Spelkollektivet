using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemies {
    public class ExpDropManager : MonoBehaviour {
        public static ExpDropManager Instance { get; private set; }


        [SerializeField] private GameObject xpDropPrefab;
        [SerializeField] private float range = 10f;
        [SerializeField] private float speed = 5f;

        private List<ExpDrop> _expDrops;
        private Player.Player _player;
        private float _playerHeight;

        // TODO: do a logic for increasing experience drops
        [SerializeField] private int currentExpDrop = 10;


        private void Awake() {
            if (Instance != null) {
                Debug.LogError("More than one instance of ExpDropManager");
            }

            Instance = this;
            this._expDrops = new List<ExpDrop>();
        }

        private void Start() {
            _player = Player.Player.Instance;
            _playerHeight = _player.GetComponent<CharacterController>().height;
        }

        // Update is called once per frame
        private void Update() {
            List<ExpDrop> expDropsInRange = GetExpDropsInRange();
            foreach (ExpDrop drop in expDropsInRange) {
                drop.transform.position = Vector3.MoveTowards(drop.transform.position,
                    _player.transform.position + Vector3.up * (_playerHeight / 2),
                    speed * Time.deltaTime);
            }

            Debug.Log("IN RANGE: " + expDropsInRange.Count);
        }

        public void SpawnExpDrop(Vector3 position, int? expAmount = null) {
            GameObject expDrop = Instantiate(xpDropPrefab, position, Quaternion.identity);
            ExpDrop expDropScript = expDrop.GetComponent<ExpDrop>();
            if (expAmount != null) {
                expDropScript.Initialize(expAmount.Value);
            }

            _expDrops.Add(expDropScript);
            Debug.Log(_expDrops.Count);
        }

        private List<ExpDrop> GetExpDropsInRange() {
            return _expDrops
                .Where(drop =>
                    drop != null && Vector3.Distance(drop.transform.position, _player.transform.position) <= range)
                .ToList();
        }
    }
}