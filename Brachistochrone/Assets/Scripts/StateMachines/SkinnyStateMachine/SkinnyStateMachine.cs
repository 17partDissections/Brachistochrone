using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


public class SkinnyStateMachine : StateMachineController<SkinnyStateMachine.SkinnyStates>
{
    public EventBus EventBus;
    [SerializeField] private Animator _animator;
    public NavMeshAgent Agent;
    //[HideInInspector] public PlayerMovement Player;

    public enum SkinnyStates
    {
        ChaseState,
        AttackState,
        GetDamageState,
    }


    [HideInInspector] public int Attack;


    private void Awake()
    {
        Attack = Animator.StringToHash("Attack");

        ChaseState ChaseState = new ChaseState(SkinnyStates.ChaseState, this);
        States.Add(SkinnyStates.ChaseState, ChaseState);


        StartMachine(SkinnyStates.ChaseState);

    }
    [Inject]
    public void Construct(EventBus eventBus/*, PlayerMovement player*/)
    {
        EventBus = eventBus;
        //Player = player;
    }
}

