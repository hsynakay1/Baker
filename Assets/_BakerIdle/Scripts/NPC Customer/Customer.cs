using System;
using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace StatueGames.BakeryIdle
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] public Animator animator;
        public ItemType selectedItem;
        public GameObject[] itemSprite;
        public Transform bag;
        public GameObject sprite;
        public GameObject spriteBacround;
        public ParticleSystem coinParticle;

        public Item itemHave;
        
        private CustomerManager customerManager;
        public Product product;
        public Vector3 currentPosition = Vector3.zero;
        private void Start()
        {
            spriteBacround.SetActive(false);
            sprite = itemSprite[0];
        }

        public void Init(CustomerManager customerManagerT)
        {
            this.customerManager = customerManagerT;
            ChooseOrder();

            gameObject.SetActive(true);

            animator.SetBool("CarryRun", false);
            CustomerRunningAnimation();

        }

        private void Update()
        {
            if((currentPosition-transform.position).sqrMagnitude < 0.001f)
            {
                CustomerIdleAnimation();
            }else{
                CustomerRunningAnimation();
            }
            currentPosition = transform.position;

            if (customerManager.lineStack.Count == 0 )
            {
                return;
            }
            if (transform.position.z - BenchProcess.Instance.transform.position.z < 5f && customerManager.lineStack[0] == this)
            {
                spriteBacround.SetActive(true);
                sprite.SetActive(false);
                sprite = itemSprite[(int)selectedItem];
                sprite.SetActive(true);
                BuyOrder();
            }
            if (gameObject.transform.position == customerManager.diePoint.transform.position)
            {
                if (bag.childCount > 0)
                {
                    Destroy(bag.GetChild(0).gameObject);
                }
               
                gameObject.SetActive(false);
            }

        }

        public void WalkToCheckpoints()
        {
            transform.LookAt(customerManager.checkpointDoor);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(customerManager.checkpointDoor.position, 2));
            sequence.OnComplete(NockNock);
        }

        public void NockNock()//I m here open the door
        {
            customerManager.lineStack.Add(this);
            customerManager.RepositionLine();
        }

        public void CustomerRunningAnimation()
        {
            animator.SetBool("isRunning",true);
            
        }

        public void CustomerIdleAnimation()
        {
            animator.SetBool("isRunning",false);
            animator.SetBool("CarryRun", false);
            animator.SetBool("CarryIdle", false);
        }

        private ItemType ChooseOrder()
        {
            selectedItem = (ItemType)Random.Range(0, GameData.UnlockProcess);
            Debug.LogWarning(GameData.UnlockProcess + "UNLOCKPROSES/////////////////");
            return selectedItem;   
        }

        public void BuyOrder()
        {
            if (bag.childCount > 0)
            {
                return;
            }
            //item buy count
            if (BenchProcess.Instance.itemPicher[(int)selectedItem].childCount > 0)
            {
                
                BenchProcess.Instance.itemPicher[(int)selectedItem].GetChild(0).transform.parent = bag.transform;
                bag.transform.GetChild(0).position = bag.transform.position;
                
                if (bag.childCount > 0)
                {
                    GameData.Coin += ((int)selectedItem + 1) ^ 2;
                    CoinManager.Instance.coin = GameData.Coin;
                    BenchProcess.Instance.coinParticle.Play();
                    
                    Invoke("LeaveLine",1f); 
                }
                BenchProcess.Instance.RepositionBench();
            }
            
        }
        // ReSharper disable once Unity.InefficientPropertyAccess
        // ReSharper disable Unity.PerformanceAnalysis
        public void LeaveLine()
        {
            animator.SetBool("CarryRun", true);
            spriteBacround.SetActive(false);
            customerManager.lineStack.Remove(this);
            transform.DOKill();
            transform.LookAt(customerManager.leavePoint);
            Sequence sequence2 = DOTween.Sequence();
            sequence2.Append(transform.DOMove(customerManager.leavePoint.position, 0.5f)
                .OnComplete(() => transform.LookAt(customerManager.exitDoorPoint.transform.position)));
            sequence2.Append(transform.DOMove(customerManager.exitDoorPoint.transform.position, 2));
            sequence2.Append(transform.DOMove(customerManager.diePoint.transform.position, 2).OnComplete(()=> Destroy(bag.GetChild(0).gameObject)));
            
            customerManager.RepositionLine();
            
        }

        
    }
    
}