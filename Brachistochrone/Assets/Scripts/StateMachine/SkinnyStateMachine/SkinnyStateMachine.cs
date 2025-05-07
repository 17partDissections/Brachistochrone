using Cysharp.Threading.Tasks;
using Q17pD.Brachistochrone.Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Q17pD.Brachistochrone.Enemy
{
    public class SkinnyStateMachine : StateMachineController<SkinnyStateMachine.SkinnyStates>
    {
        private NavMeshAgent _agent;
        private Animator _animator;
        private int _running = Animator.StringToHash("Running");
        private int _attack = Animator.StringToHash("Attack");
        private int _rage = Animator.StringToHash("Rage");
        private SceneGrid _sceneGrid;
        private PlayerMovement _playerMovement;
        private EventBus _bus;

        private bool _isPlayerInTrigger;
        [Range(1, 4)][SerializeField] private int _skinnyDamage;
        [Range(1, 20)][SerializeField] private int _skinnyChaseMaxNecessarySeconds;

        public enum SkinnyStates
        {
            ChasingState,
            AttackingState,
        }
        [Inject] private void Construct(PlayerMovement playerMovement, EventBus bus)
        {
            _animator = GetComponent<Animator>();
            _playerMovement = playerMovement;
            _bus = bus;
           _agent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            ChasingState ChasingState = new ChasingState(SkinnyStates.ChasingState, this);
            States.Add(SkinnyStates.ChasingState, ChasingState);
            //PatrolingState PatrolingState = new PatrolingState(SkinnyStates.PatrolingState, this);
            //States.Add(SkinnyStates.PatrolingState, PatrolingState);
            //GoingToPlayerState GoingToPlayerState = new GoingToPlayerState(SkinnyStates.GoingToPlayerState, this);
            //States.Add(SkinnyStates.GoingToPlayerState, GoingToPlayerState);
            //ScreamingState ScreamingState = new ScreamingState(SkinnyStates.ScreamingState, this);
            //States.Add(SkinnyStates.ScreamingState, ScreamingState);

            //StartMachine(SkinnyStates.WaitingState); CheckAgentVelocityAsync().Forget();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMovement>() != null)
            {
                //ChangeStateAction(SkinnyStates.AttackingState);
            }
        }
        private async UniTaskVoid CheckAgentVelocityAsync()
        {
            while (true)
            {
                await UniTask.WaitUntilValueChanged(this, x => x._agent.velocity.magnitude > 0.1f);
                _animator.SetBool(_running, false);
                await UniTask.WaitWhile(() => _agent.velocity.magnitude > 0);
                _animator.SetBool(_running, true);
            }
        }

    }
}

