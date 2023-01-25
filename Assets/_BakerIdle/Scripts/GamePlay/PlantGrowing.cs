using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace StatueGames.BakeryIdle
{
    public class PlantGrowing : MonoBehaviour
    {
        public float growthTime;
        public float collectionTime = 1;
        public Item wheatGrain;
        public bool isThere;

        private void Start()
        {
            FieldProcess.Instance.plants.Add(this);
            isThere = true;
        }

        public void GrowingPlant()
        {
            gameObject.transform.DOScale(1, growthTime);
            isThere = true;
            
        }

        public void DestroyPlant()
        {
            gameObject.transform.DOScale(0, collectionTime).OnComplete(() => GrowingPlant());
            
            isThere = false;
        }
    }
}