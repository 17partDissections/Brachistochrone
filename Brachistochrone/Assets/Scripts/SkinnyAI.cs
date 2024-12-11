using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class SkinnyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    private PlayerMovement _player;

    [Inject] private void Construct(PlayerMovement player)
    {
        _player = player;
    }
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_player.transform.position);
    }
}
