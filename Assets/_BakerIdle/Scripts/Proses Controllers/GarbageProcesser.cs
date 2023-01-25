using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using StatueGames.BakeryIdle;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public class GarbageProcesser : Processer
    {
        public Transform garbageCollector;
        public Item currentItem;
        public Transform garbageCover;
        

        public override void Activate()
        {
            // distance fix;
            if (Vector3.SqrMagnitude(transform.position - PlayerMovement.Instance.transform.position) > 6f)
            {
                return;
            }
            base.Activate();
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(garbageCover.DOLocalRotate(new Vector3(-45f, 0f, 0f), 0.5f));


            sequence.Append(garbageCover.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.5f));

            for (int i = 0; i < 6; i++)
            {
                currentItem = PlayerMovement.Instance.GetItemFromStack((ItemType)(i));
                
                if (currentItem != null)
                {
                    currentItem.transform.parent = garbageCollector.transform;
                    currentItem.transform.DOMove(garbageCollector.position, 1);
                        Destroy(currentItem.gameObject,1);
                        Debug.Log("Destroy(currentItem.gameObject)");
                    
                    
                    
                    
                    Debug.LogWarning(currentItem.name + "garbage***************-------***********");
                }
            }
        }
    }
}