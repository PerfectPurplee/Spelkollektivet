using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : MonoBehaviour
    {
    
        public Transform target;
        public float attackDistance;
    
        private NavMeshAgent _agent;
        private Animator _animator;
        private float _distance;
    
    
        void Start()
        {
            this._agent = GetComponent<NavMeshAgent>();
            this._animator = GetComponent<Animator>();
        
        }

        void Update()
        {
            this._distance = Vector3.Distance(this.transform.position, this.target.position);
        
        
        
        }

        private void OnAnimatorMove()
        {
            this._agent.SetDestination(this.target.position);
        }
    }
}
