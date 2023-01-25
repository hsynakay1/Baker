using System;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public class CoinManager : MonoBehaviour
    {
        public int coin;

        public static CoinManager Instance;

        private void Awake()
        {
            Instance = this;
            coin = GameData.Coin;
        }
    }
}