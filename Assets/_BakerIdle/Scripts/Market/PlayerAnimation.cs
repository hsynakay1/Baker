using System;
using StatueGames.BakeryIdle;
using UnityEngine;

namespace _BakerIdle.Scripts.Market
{
    public class PlayerAnimation : MonoBehaviour
    {
        public Animator playerAnimation;

        private void Start()
        {
            playerAnimation = GetComponentInChildren<Animator>();
            PlayerMovement.Instance.playerAnimation = playerAnimation;
        }
    }
}