using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StatueGames.BakeryIdle
{
    public class GoatAnimations : MonoBehaviour
    {
        public GameObject manger;
        public Animator animator;
        public MilkBucket milkBucket;
        private static readonly int Animation1 = Animator.StringToHash("animation");
        private bool changeAnim;

        private void Start()
        {
            StartCoroutine(ChangeAnimation());
        }

        private void Update()
        {
            if (manger.transform.childCount > 0 && !milkBucket.isFull)  
            {
                animator.SetInteger(Animation1,4);
                StartCoroutine(DestroyChiltObje());
            }
            else if(changeAnim)
            {
                int i = Random.Range(0, 2);
                animator.SetInteger(Animation1, i == 0 ? 5 : 0);
                changeAnim = false;
            }
            
            
        }

        IEnumerator DestroyChiltObje()
        {
            yield return new WaitForSeconds(2);
            if (manger.transform.childCount <= 0)
            {
                yield break;
            }
            Destroy(manger.transform.GetChild(0).gameObject);//***********

            yield return new WaitForSeconds(0.5f);
            milkBucket.FillEmptyBucket(true);

        }

        IEnumerator ChangeAnimation()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(Random.Range(5,16));
                if (!changeAnim)
                {
                    changeAnim = true;
                }
            }
        }
    }
}