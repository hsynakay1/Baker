using System;
using System.Collections;
using System.Collections.Generic;
using _BakerIdle.Scripts.Market;
using StatueGames;
using StatueGames.BakeryIdle;
using StatueGames.Foundation;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMarket : MonoBehaviour
{
    public GameObject currentAvatar;
    public Transform avatarHolder;
    public MarketData marketData;
    public PlayerAnimation _playerAnimation;

    private void Start()
    {
        EventManager.Instance.playerShowSkin += SkinChange;
        SkinChange(GameData.SavePlayerAvatar);
    }

    public void SkinChange(int skinIndex)
    {
        if (currentAvatar != null)
        {
            DeleteAvatar();
        }

        currentAvatar = Instantiate(marketData.marketItemDataes[skinIndex].prefab, avatarHolder);
        currentAvatar.transform.position = avatarHolder.position;
        _playerAnimation = currentAvatar.GetComponent<PlayerAnimation>();
        PlayerMovement.Instance.playerAnimation = _playerAnimation.GetComponent<Animator>();
        
    }

    public void DeleteAvatar()
    {
        Destroy(currentAvatar);
    }
}
