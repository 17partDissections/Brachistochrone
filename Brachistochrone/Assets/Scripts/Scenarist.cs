using Q17pD.Brachistochrone.Enemy;
using Q17pD.Brachistochrone.Items;
using Q17pD.Brachistochrone.Player;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Q17pD.Brachistochrone
{
    public class Scenarist
    {
        private SceneGrid _sceneGrid;
        private Skinny _skinny;
        private List<ItemID> _stolenItems;
        private GameObject _playerObject;

        public Scenarist(SceneGrid grid, EventBus eventBus, PlayerMovement playerMovement, Skinny skinny)
        {
            _skinny = skinny;
            _sceneGrid = grid;
            _playerObject = playerMovement.gameObject;
            eventBus.FearFullEvent += SpawnMonster;
            //Debug.Log("Scenarist is enabled. Skinny = " + _skinny);
        }
        private void SpawnMonster()
        {
            Room room = GetPlayerNeighbourRoom();
            _skinny.gameObject.SetActive(true);
            _skinny.transform.position = room.MonsterSpawnTransforms[Random.Range(0, room.MonsterSpawnTransforms.Count)].position;
            _skinny.Coroutine = _skinny.StartCoroutine(_skinny.ChaseCoroutine());
        }
        private Room GetPlayerNeighbourRoom()
        {
            _sceneGrid.GetNode(_sceneGrid.TranscodeVectorToNode(_playerObject.transform.position), out Node playerNode);
            List<Node> nodes = _sceneGrid.GetBusyNeighboursNodes(playerNode);
            return nodes[Random.Range(0, nodes.Count)].Room;
        }
    }
}
