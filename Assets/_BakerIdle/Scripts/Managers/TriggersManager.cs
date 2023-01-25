using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using StatueGames.BakeryIdle;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    enum Distance
    {
        MilkMakerDistance,
        FieldDistance,
        BenchDistance,
        IceCreamDistance
    }
    public class TriggersManager : MonoBehaviour
    {
        private Distance _distance;
        private PlayerMovement _playerMovement;
        
        public static TriggersManager Instance;
        public List<Processer> processersList;
        public bool isTrigger;
        public Processer newProcess;
        
        private void Awake() 
        {
            Instance = this;
        }
        void Start()
        {
            _playerMovement = PlayerMovement.Instance;
        }

        void Update()
        {
            float distance = 16f;
            int processId = -1;
            for (int i = 0; i < processersList.Count; i++)
            {
                newProcess = processersList[i];
                float newDistance =    
                    Vector3.SqrMagnitude(_playerMovement.transform.position - newProcess.gameObject.transform.position);
                if(newDistance < distance)
                {
                    distance = newDistance;
                    processId = i;
                    isTrigger = true;
                }
                isTrigger = false;
            }
            if(processId != -1)
            {
                processersList[processId].Activate();
            }
        }

        private void TriggerSelector()
        {
            switch (_distance)
            {
                case Distance.MilkMakerDistance:
                    MilkMaker();
                    break;
                case Distance.FieldDistance:
                    Field();
                    break;
                case Distance.BenchDistance:
                    Bench();
                    break;
                case Distance.IceCreamDistance:
                    IceCream();
                    break;
            }
        }

        public void IceCream()
        {
            Debug.Log("keci");
        }
        public void Field()
        {
            Debug.Log("tarla");
        }
        public void Bench()
        {
            Debug.Log("tezgah");
        }
        public void MilkMaker()
        {
            Debug.Log("dondurma");
        }
    }
}
