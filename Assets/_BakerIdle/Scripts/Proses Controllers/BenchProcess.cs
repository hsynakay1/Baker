using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace StatueGames.BakeryIdle
{


    public class BenchProcess : Processer
    {
        public Transform[] itemPicher;
        public static BenchProcess Instance;
        public Item currentItem;
        public ParticleSystem coinParticle;

        private void Awake()
        {
            Instance = this;
        }

        public override void Activate()
        {
            base.Activate();
            for (int j = 0; j < 6; j++)
            {
                currentItem = PlayerMovement.Instance.GetItemFromStack((ItemType)(j));
                
                if (currentItem != null)
                {
                    currentItem.transform.position = itemPicher[j].position;
                    currentItem.transform.parent = itemPicher[j].transform;
                }
                RepositionBench();
            }
            
        }

        public void RepositionBench()
        {
            for (int k = 0; k < itemPicher.Length; k++)
            {
                for (int l = 0; l < itemPicher[k].childCount; l++)
                {
                    itemPicher[k].GetChild(l).transform.position = itemPicher[k].position + Vector3.up * (l * 0.5f);
                }
            }
        }

    }
}