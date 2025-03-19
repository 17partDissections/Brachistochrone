using Q17pD.Brachistochrone.Items;
using UnityEngine;

namespace Q17pD.Brachistochrone.Factories
{
    public interface IFactory 
    {
        public InGameItem Create(ItemID neededItem);
    }
}
