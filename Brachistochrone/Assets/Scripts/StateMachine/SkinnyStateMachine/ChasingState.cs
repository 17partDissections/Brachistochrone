using System.Collections;
using Q17pD.Brachistochrone.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Q17pD.Brachistochrone.Enemy
{
    public class ChasingState : BaseState<SkinnyStateMachine.SkinnyStates>
    {
        private SkinnyStateMachine _stateMachine;
        private Animator _animator;
        private NavMeshAgent _agent;
        private PlayerMovement _playerMovement;
        private int _rage;
        private bool _attacking;
        private float _skinnyChaseMaxNecessarySeconds;
        private bool _isHuntCanStop;
        public ChasingState(SkinnyStateMachine.SkinnyStates state,
            SkinnyStateMachine CurrentStateMachine) : base(state)
        {
            _stateMachine = CurrentStateMachine;
        }

        public override void Enter2State()
        {
            //StartCoroutine(ChaseCoroutine());
        }

        public override void Exit2State()
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
        private IEnumerator ChaseCoroutine()
        {
            _animator.SetTrigger(_rage);
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSeconds(stateInfo.length);
            float i = 0;
            while (i != _skinnyChaseMaxNecessarySeconds)
            {
                if (_attacking == false)
                    _agent.SetDestination(_playerMovement.transform.position);
                yield return null;
                i += Time.deltaTime;
            }
            _isHuntCanStop = true;
            while (true)
            {
                if (_attacking == false)
                    _agent.SetDestination(_playerMovement.transform.position);
                yield return null;
            }
        }
    }
}
