using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using StatueGames.BakeryIdle;
using UnityEngine;

namespace StatueGames.BakeryIdle
{


    public class CoffeProcesser : Processer
    {
        private void Update()
        {
            PullImport();
        }
        public override void Import()
        {
            base.Import();

            //Change
            if(ImportSpotFull())
                return;

            Item temp = PlayerMovement.Instance.GetItemFromStack(ItemType.Milk);
            if (temp != null)
            {
                feederCount = ReturnEmptyImportSpot();
                if (feederCount == -1)
                    return;

                temp.gameObject.transform.DOMove(importPickers[feederCount].transform.position, 1);
                temp.transform.parent = importPickers[feederCount].transform;
            }

        }

        public override void Export()
        {
            base.Export();
            for (int i = 0; i < exportPickers.Length; i++)
            {
                if (exportPickers[i].childCount > 0)
                {   
                    exportCache = exportPickers[i].GetChild(0).GetComponent<Item>();
                    PlayerMovement.Instance.PutItemInToStack(exportCache);
                    return;
                }
            }
        }

    }
}