using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkBucket : MonoBehaviour
{
    public GameObject milkBucketsEmpty;
    [SerializeField]private GameObject milkBucketsFull;
    public bool isFull;

    public void FillEmptyBucket(bool isFull)
    {
        this.isFull = isFull;
        milkBucketsEmpty.SetActive(!isFull);
        milkBucketsFull.SetActive(isFull);
    }
}
