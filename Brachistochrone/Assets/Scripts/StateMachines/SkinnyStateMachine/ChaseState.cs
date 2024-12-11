using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class ChaseState : BaseState<SkinnyStateMachine.SkinnyStates>
{
    private SkinnyStateMachine _stateMachine;
    public ChaseState(SkinnyStateMachine.SkinnyStates state,
        SkinnyStateMachine CurrentStateMachine) : base(state)
    {
        _stateMachine = CurrentStateMachine;
    }

    public override void Enter2State()
    {
        Debug.Log(_stateMachine.EventBus);
        //_stateMachine.EventBus.ChaseEvent.Invoke();
    }

    public override void Exit2State()
    {

    }

    public override void UpdateState()
    {
        //_stateMachine.Agent.SetDestination(_stateMachine.Player.transform.position);
        if (_stateMachine.Agent.remainingDistance <0.5f)
        {
            ChangeStateAction(SkinnyStateMachine.SkinnyStates.AttackState);
        }

    }

    
}
