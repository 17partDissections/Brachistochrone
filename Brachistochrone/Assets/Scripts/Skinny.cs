using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Q17pD.Brachistochrone.Player;
using Cysharp.Threading.Tasks;
using Q17pD.Brachistochrone.Items;

namespace Q17pD.Brachistochrone.Enemy
{
    public class Skinny : MonoBehaviour
    {
        [Range(1, 4)][SerializeField] private int _skinnyDamage;
        [Range(1, 20)][SerializeField] private int _skinnyChaseMaxNecessarySeconds;
        private bool _isChaseCanStop;
        private NavMeshAgent _agent;
        public Coroutine Coroutine;
        private Animator _animator;
        private int _running = Animator.StringToHash("Running");
        private int _attack = Animator.StringToHash("Attack");
        private SceneGrid _sceneGrid;
        private PlayerMovement _playerMovement;
        private bool _isPlayerInTrigger;

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _animator = GetComponent<Animator>();
            _playerMovement = playerMovement;
        }
        public IEnumerator ChaseCoroutine()
        {
            float i = 0;
            while (i != _skinnyChaseMaxNecessarySeconds)
            {
                _agent.SetDestination(_playerMovement.transform.position);
                yield return null;
                i += Time.deltaTime;
            }
            _isChaseCanStop = true;
            while (true)
            {
                _agent.SetDestination(_playerMovement.transform.position);
                yield return null;
            }
        }
        public void CheckChaseStatus()
        {
            if (_isChaseCanStop) { StopCoroutine(Coroutine); _isChaseCanStop = false; }
        }
        private void OnColliderEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>() != null)
            {

            }
        }
        private void OnColliderExit(Collider other) { if (other.GetComponent<PlayerMovement>() != null) _isPlayerInTrigger = false; }
        private async UniTask CheckAgentVelocityAsync()
        {
            while (true)
            {
                await UniTask.WaitUntilValueChanged(this, x => x._agent.velocity.magnitude > 0.1f);
                _animator.SetBool(_running, true);
                await UniTask.WaitWhile(() => _agent.velocity.magnitude > 0);
                _animator.SetBool(_running, false);
            }
        }
    }
}

