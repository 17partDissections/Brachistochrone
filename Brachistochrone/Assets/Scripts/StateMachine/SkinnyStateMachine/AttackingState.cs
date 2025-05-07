using System.Collections;
using Q17pD.Brachistochrone.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Q17pD.Brachistochrone.Enemy
{
    public class AttackingState : BaseState<SkinnyStateMachine.SkinnyStates>
    {
        private SkinnyStateMachine _stateMachine;
        private NavMeshAgent _agent;
        private Animator _animator;
        private int _attack;
        private bool _isPlayerInTrigger;
        private bool _attacking;

        public AttackingState(SkinnyStateMachine.SkinnyStates state,
            SkinnyStateMachine CurrentStateMachine) : base(state)
        {
            _stateMachine = CurrentStateMachine;
        }

        public override void Enter2State()
        {
            _isPlayerInTrigger = true;
            _attacking = true;
        }

        public override void Exit2State()
        {
            _attacking = false;
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }
        private IEnumerator AttackCoroutine()
        {
            _agent.Stop();
            _animator.SetTrigger(_attack);
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log(stateInfo.length);
            yield return new WaitForSeconds(stateInfo.length);
        }
    }
}
