using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatueGames.Foundation
{

    public class MarketManager : MonoBehaviour
    {

        public MarketData marketItemData;
        public MarketData marketAvatarData;

        public static MarketManager Instance;
        [HideInInspector]
        public MarketItem currenMarketSelectedItem;
        public MarketItem prefab;
        public Transform holder;
        //[HideInInspector]
        public List<MarketItem> marketItems;
        public string currenMarket;
        
        private void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            marketItems = new List<MarketItem>();
            for (int i = 0; i < 30; i++)
            {
                MarketItem marketItem = Instantiate(prefab, holder);
                marketItems.Add(marketItem);
                marketItem.InitItem();
            }
            Click_AvatarMarket();
        }

        public void Select(int id)
        {

            Debug.LogWarning(currenMarket);
            if (currenMarket == "Avatar")
            {
                GameData.Select_Icon = id;
                Debug.LogWarning(id);
            }
            else
            {
                GameData.SavePlayerItem = id;
            }
        }

        public void Click_ItemMarket()
        {
            currenMarketSelectedItem?.UnSelect();
            currenMarketSelectedItem = null;
            ChangeMarket(marketItemData);
            marketItems[GameData.SavePlayerItem].Select();
        }
        public void Click_AvatarMarket()
        {
            currenMarketSelectedItem?.UnSelect();
            currenMarketSelectedItem = null;
            ChangeMarket(marketAvatarData);
            marketItems[GameData.Select_Icon].Select();
        }

        public void ChangeMarket(MarketData marketData)
        {
            currenMarket = marketData.marketDataName;
            for (int i = 0; i < 30; i++)
            {
                if (i >= marketData.marketItemDataes.Length)
                {
                    marketItems[i].gameObject.SetActive(false);
                    continue;
                }
                marketData.marketItemDataes[i].id = i;
                MarketItem marketItem = marketItems[i];
                marketItem.dataSaveString = "item {0}";
                marketItem.dataSaveString = marketData.name + "item {0}";
                marketItem.marketItemData = marketData.marketItemDataes[i];
                marketItem.SetList();
                marketItems[i].gameObject.SetActive(true);
            }
        }

        public void InvokeEvent(int eventIndex)
        {
            if (currenMarket == "Avatar")
            {
                EventManager.Instance.PlayerSkinShown(eventIndex);
                GameData.SavePlayerAvatar = eventIndex;
            }
            else
            {
                EventManager.Instance.PlayerShowItem(eventIndex);
                GameData.SavePlayerItem = eventIndex;
            }
            
        }

    }
}