using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
   
namespace StatueGames.BakeryIdle
{ 
    public class FieldProcess : Processer
    {
        public List<PlantGrowing> plants;
        public static FieldProcess Instance;

        private Item wheatGrain;
        public Item wheatGrainPrefab;
        private int lastCollectedField;
        private PlantGrowing currentPlant;
        private void Awake()
        {
            Instance = this;
        }
        
        public override void Activate()
        {
            base.Activate();
            if (PlayerMovement.Instance.playerEmptyCapacity > 0)
            {
                lastCollectedField++;
                currentPlant = plants[lastCollectedField % plants.Count];
                if (currentPlant.isThere)
                {
                    currentPlant.DestroyPlant();
                    
                    wheatGrain = Instantiate(wheatGrainPrefab, currentPlant.transform);
                    wheatGrain.isInPlayer = true;
                    
                    PlayerMovement.Instance.PutItemInToStack(wheatGrain);
                }
            }
        }
    }
}