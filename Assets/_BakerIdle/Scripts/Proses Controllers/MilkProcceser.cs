using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public class MilkProcceser : Processer
    {
        public Transform[] wheatPickers;
        public MilkBucket[] milkBuckets;

        public static MilkProcceser Instance;

        private Item milk;
        public Item milkPrefab;
        

        private void Awake()
        {
            Instance = this;
        }

        public override void Activate()
        {
            base.Activate();
        }
        public override void Import()
        {
            base.Import();
            if (WheatSpotFull())
                return;
            
            Item temp = PlayerMovement.Instance.GetItemFromStack(ItemType.Wheat);
            
            if (temp != null )
            {
                feederCount ++;
                feederCount %= wheatPickers.Length;
                temp.gameObject.transform.DOMove(wheatPickers[feederCount].transform.position, 1);
                temp.transform.parent = wheatPickers[feederCount].transform;
            }
        }
        public override void Export()
        {
            base.Export();
            foreach (MilkBucket currentMilk in milkBuckets)
            {
                if(currentMilk.isFull)
                {
                    currentMilk.FillEmptyBucket(false);
                    milk = Instantiate(milkPrefab, pickerCenter.position,Quaternion.identity);
                    PlayerMovement.Instance.PutItemInToStack(milk);
                    return;
                }
            }
        }
        public bool WheatSpotFull()
        {
            int emptyItemSpots = 0;
            for (int i = 0; i < wheatPickers.Length; i++)
            {
                if(0 == wheatPickers[i].transform.childCount)
                    emptyItemSpots++;
            }
            return emptyItemSpots == 0;
        }
        
    }
}
