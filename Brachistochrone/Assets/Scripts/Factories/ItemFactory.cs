
using System.Linq;
using Q17pD.Brachistochrone.Items;
using UnityEngine;
using Zenject;

namespace Q17pD.Brachistochrone.Factories
{
    public class ItemFactory : IFactory
    {
        private ItemHolder _itemHolder;
        private DiContainer _container;

        public ItemFactory(ItemHolder itemHolder, DiContainer container)
        {
            _itemHolder = itemHolder;
            _container = container;
        }
        public InGameItem Create(ItemID itemID)
        {
            
            InGameItem neededItem = GameObject.Instantiate(_itemHolder.Items.FirstOrDefault(x=>x.ItemID == itemID));
            if (neededItem != null)
            {
                _container.Inject(neededItem);
                return neededItem;
            }
            else 
                throw new System.Exception("theres no item with that ItemID: " +  itemID);
                
        }
    }
}


