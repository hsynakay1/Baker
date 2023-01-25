using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public class Item : MonoBehaviour
    {
        public bool isInPlayer;
        
        public ItemType itemType;
        
        public void SetType(ItemType type)
        {
            itemType = type;
        }

    
    }
    public enum ItemType
    {
        Wheat,
        Milk,
        Coffe,
        IceCream,
        Fame,
        Cake
    }
}