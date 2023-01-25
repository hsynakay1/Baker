using System;
using System.Collections;
using System.Collections.Generic;
using StatueGames.BakeryIdle;
using UnityEngine;

public class ProductLaunchManager : MonoBehaviour
{
    public GameObject[] offProducts;
    public int produckPrice;
    private CoinManager coinManager;

    private void Awake()
    {
        coinManager = CoinManager.Instance;
    }

    void Start()
    {
        foreach (GameObject products in offProducts)
        {
            products.SetActive(false);
        }
    }
}
