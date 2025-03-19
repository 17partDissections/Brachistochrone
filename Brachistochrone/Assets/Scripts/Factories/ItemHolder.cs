using Q17pD.Brachistochrone.Items;
using UnityEngine;
using System.Collections.Generic;

namespace Q17pD.Brachistochrone
{
    [CreateAssetMenu(fileName = "New ItemHolder", menuName = "Scriptable Objects/ItemHolder")]
    public class ItemHolder : ScriptableObject
    {
        public List<InGameItem> Items;
    }
}
