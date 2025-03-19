using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Q17pD.Brachistochrone.Items;
using UnityEngine;
using Zenject;

namespace Q17pD.Brachistochrone.Factories
{
    public class ItemObjectPool
    {
        private IFactory _itemFactory;
        public Dictionary<ItemID, List<InGameItem>> Items = new Dictionary<ItemID, List<InGameItem>>();

        public ItemObjectPool(IFactory factory, ItemHolder itemHolder, int initObjectCountPerId = 1)
        {
            _itemFactory = factory;
            foreach (var items in itemHolder.Items)
            {
                for (int i = 0; initObjectCountPerId > i; i++)
                {
                    CreateInstance(items.ItemID);
                }
            }
        }
        private InGameItem CreateInstance(ItemID itemID)
        {
            InGameItem createdItem = null;

            if (Items.ContainsKey(itemID))
            {
                createdItem = _itemFactory.Create(itemID);
                Items[itemID].Add(createdItem);
            }
            else
            {
                createdItem = _itemFactory.Create(itemID);
                Items.Add(itemID, new List<InGameItem>() { createdItem });
            }


            createdItem.gameObject.SetActive(false);

            return createdItem;
        }

        public InGameItem GetFromPool(ItemID itemID)
        {
            InGameItem inGameItem = Items[itemID].FirstOrDefault(x=>!x.gameObject.activeSelf);
            if (inGameItem == null)
                inGameItem = CreateInstance(itemID);
            inGameItem.gameObject.SetActive(true);
            return inGameItem;
        }
        public void DropBackToPool(InGameItem inGameItem)
        {
            inGameItem.gameObject.SetActive(false);
        }
    }
}
