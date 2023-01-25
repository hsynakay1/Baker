using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StatueGames.Foundation;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public enum GameStade
    {
        UI,
        GamePlay
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        private EventManager _eventManager;
        public GameStade gameStade;

        public CinemachineVirtualCamera UICam;
        public CinemachineVirtualCamera GamePlayCam;
        public GameObject joystickCanvas;
        

        private float _turnSpeed;
        private float _speed;

        private void Awake()
        {
            _eventManager = EventManager.Instance;
            gameStade = GameStade.UI;
            Instance = this;
        }

        private void Start()
        {
            _eventManager.levelStart += GamePlayState;
            _eventManager.inGameMenu += UIState;

            PlayerMovement.Instance.turnSpeed = _turnSpeed;
            PlayerMovement.Instance.turnSpeed = _speed;
        }
        
        public void UIState(bool inMarket)
        {
            UICam.Priority = 20;
            joystickCanvas.SetActive(false);
            PlayerMovement.Instance.turnSpeed = 0;
            PlayerMovement.Instance.speed = 0;
            PlayerMovement.Instance.transform.Rotate(new Vector3(0,180,0));
            CustomerManager.Instance.line = false;
            Debug.LogWarning(gameStade);
        }
        private void GamePlayState()
        {
           
            UICam.Priority = 1;
            joystickCanvas.SetActive(true);
            PlayerMovement.Instance.turnSpeed = 200;
            PlayerMovement.Instance.speed = 5;
            CustomerManager.Instance.line = true;
            Debug.LogWarning(gameStade);
        }
       
        //UI -> cinemachine
        //gameplay -> karakter hareketi yok, yeni müşteri gelmeyecek

    }
}