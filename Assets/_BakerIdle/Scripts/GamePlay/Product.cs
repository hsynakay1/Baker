using System;
using DG.Tweening;
using StatueGames.Foundation;
using TMPro;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public enum ProsesNames
    {
        Field,
        Goat,
        Flour,
        Coffee,
        IceCream,
        Cake
    }
    public class Product : MonoBehaviour
    {
        public ProsesNames prosesName;
        public TextMeshPro priceText;
        [SerializeField]private int _price;
        
        [SerializeField] private ParticleSystem particle;

        [Header("Processers")]
        public Transform unlockItem;
        public GameObject unlockSprite;

        public Transform activateObject;
        public Transform deactiveObject;
        [SerializeField]private int unlockId;
        private Vector3 scaleStart;
        private bool unLocked;
        private String saveString;
        
        private void Start()
        {
            saveString = gameObject.name+"Product"+unlockId;
            priceText.text = (unlockId*5).ToString();

            unLocked = PlayerPrefs2.GetBool(saveString);

            unlockItem.gameObject.SetActive(unLocked);
            unlockSprite.SetActive(false);

            scaleStart = unlockItem.localScale;
            
            if (activateObject != null)
                activateObject.gameObject.SetActive(GameData.SaveCoffeeProcess);
            
            if (deactiveObject != null)
                deactiveObject.gameObject.SetActive(GameData.SaveIceCreamProcess);
            
           
        }
        private void Update()
        {
            
            DistanceMeter();
        }

        public void DistanceMeter()
        {
            if(unlockItem.gameObject.activeInHierarchy)
                return;
            
            if(GameData.UnlockProcess < unlockId)
                return;
            
            if(GameData.UnlockProcess >= unlockId && !unlockSprite.activeInHierarchy)
            {
                unlockSprite.SetActive(true);
            }


            float distance = Vector3.SqrMagnitude(PlayerMovement.Instance.transform.position - unlockSprite.transform.position);

            if (CoinManager.Instance.coin > _price && distance < 5f)
            {
                unlockItem.gameObject.SetActive(true);
                GameData.UnlockProcess++;
                Debug.LogWarning(GameData.UnlockProcess+gameObject.name);
                unlockItem.transform.localScale = Vector3.zero;
                unlockItem.DOScale(scaleStart, 1.5f);
                CoinManager.Instance.coin -= _price;
                GameData.Coin -= _price;
                unlockSprite.SetActive(false);
                particle.Play();
                PlayerPrefs2.SetBool(saveString,true);

                if (activateObject != null)
                {
                    GameData.SaveCoffeeProcess = true;
                    activateObject.gameObject.SetActive(GameData.SaveCoffeeProcess);
                }
                if (deactiveObject != null)
                {
                    GameData.SaveIceCreamProcess = false;
                    deactiveObject.gameObject.SetActive(GameData.SaveIceCreamProcess);
                }
                
                
            }
        }
            
    } 
}