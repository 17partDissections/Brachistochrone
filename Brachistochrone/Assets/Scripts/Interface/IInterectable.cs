
using Q17pD.Brachistochrone.Player;
using Q17pD.Brachistochrone.Player.Canvas;
using UnityEngine;

namespace Q17pD.Brachistochrone
{
    public interface IInterectable
    {
        public void Interact(PlayerInventory inventory = null, PlayerCanvas playerCanvas = null);
    }
}
