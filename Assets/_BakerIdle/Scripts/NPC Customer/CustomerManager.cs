using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StatueGames.BakeryIdle
{
    public class CustomerManager : MonoBehaviour
    {
        public Transform customerBase1;
        public Transform customerBase2;

        public Transform checkpointDoor;
        public Transform checkpointBench;

        public List<Customer> lineStack = new List<Customer>();
        public Transform buyPoint;

        [SerializeField] 
        private Customer[] customerArray;
        private Customer customer;
        private int customerSpawnCount;
        private Transform _linePos;
        public bool line;
        public static CustomerManager Instance;
        
        public Transform leavePoint;
        public Transform exitDoorPoint;
        public Transform diePoint;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            StartCoroutine(SpawnTimer(3));
            customerSpawnCount = Random.Range(0,customerArray.Length);
        }

        private void Update()
        {
            line = lineStack.Count <= 9;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void SpawnCustomer()
        {
            customerSpawnCount ++;
            customerSpawnCount %= customerArray.Length;
            customer = customerArray[customerSpawnCount];
            customer.Init(this);
            

            int i = Random.Range(0, 2);
            customer.transform.position = i == 0 ? customerBase1.position : customerBase2.position;
            customer.WalkToCheckpoints();
        }

        
        public void RepositionLine()
        {
            Debug.LogWarning("RepositionLine");
            
            for (int i = 0; i < lineStack.Count; i++)
            {
                lineStack[i].transform.DOKill();
                lineStack[i].transform.DOLookAt(checkpointBench.position, 0.1f);
                lineStack[i].transform.DOMove(buyPoint.position + Vector3.forward * (i + 0.5f), 2f).OnComplete(customer.CustomerIdleAnimation);
                
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator SpawnTimer(float time)
        {
            while (true)
            {
                if (line)
                {
                    yield return new WaitForSeconds(time);
                    SpawnCustomer();
                }

                yield return null;
            }
        }
    }
}