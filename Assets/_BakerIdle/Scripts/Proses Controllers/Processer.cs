using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace StatueGames.BakeryIdle
{
    public class Processer : MonoBehaviour
    {
        public Transform importer;
        public Transform exporter;
        
        private float time;

        [HideInInspector]
        public Item exportCache;
        public Item exportPrefab;
        public int feederCount;

        private int _coffeePositionNumber = 0;
        public float processTime;
        public Transform pickerCenter;

        public Transform[] importPickers;
        public Transform[] exportPickers;

        public virtual void Start()
        {
            Invoke("ActivateProduct",1.6f);
        }

        public void ActivateProduct()
        {
            TriggersManager.Instance.processersList.Add(this);
        }
        public virtual void Activate()
        {
            if (Time.realtimeSinceStartup - time < 0.5f)
            {
                return;
            }
            if(importer == null ){
                Export();
                return;
            }

            Vector3 position = PlayerMovement.Instance.transform.position;
            float distExport = Vector3.SqrMagnitude(position-exporter.position);
            float distImport = Vector3.SqrMagnitude(position-importer.position);
            
            if(distExport>distImport)
            {
                Import();
            }
            else if(PlayerMovement.Instance.playerEmptyCapacity > 0 )
            {
                Export();
            }
        }

        public virtual void Import()
        {
            time = Time.realtimeSinceStartup;
        }

        public virtual void Export()
        {
            time = Time.realtimeSinceStartup;
        }

        //Change
        public bool ImportSpotFull()
        {
            int emptyItemSpots = 0;
            for (int i = 0; i < importPickers.Length; i++)
            {
                if(0 == importPickers[i].transform.childCount)
                    emptyItemSpots++;
            }
            return emptyItemSpots == 0;
        }
        public bool ExportSpotFull()
        {
            int emptyItemSpots = 0;
            for (int i = 0; i < importPickers.Length; i++)
            {
                if(0 == exportPickers[i].transform.childCount)
                    emptyItemSpots++;
            }
            return emptyItemSpots == 0;
        }

        public int ReturnEmptyExportSpot()
        {
            for (int i = 0; i < exportPickers.Length; i++)
            {
                if(0 == exportPickers[i].transform.childCount)
                    return i;
            }
            return -1;
        }
        public int ReturnEmptyImportSpot()
        {
            for (int i = 0; i < importPickers.Length; i++)
            {
                if(0 == importPickers[i].transform.childCount)
                    return i;
            }
            return -1;
        }

        public void PullImport()
        {
            if (Time.realtimeSinceStartup - processTime < 2.5f)
            {
                return;
            }

            for (int i = 0; i < importPickers.Length; i++)
            {
                if (importPickers[i].transform.childCount > 0 && !ExportSpotFull())//Change
                {
                    Transform importProduct = importPickers[i].transform.GetChild(0);
                    importProduct.DOMove(pickerCenter.position, 1);
                    importProduct.parent = pickerCenter.transform;
                    Destroy(importProduct.gameObject, 2);
                    processTime = Time.realtimeSinceStartup;
                    StartCoroutine(WaitTurn(2));
                    return;
                }
            }
        }
        public void PushExport()
        {
            _coffeePositionNumber = ReturnEmptyExportSpot();
            if(_coffeePositionNumber == -1)
                return;
            Item exportProduct = Instantiate(exportPrefab, pickerCenter);
            exportProduct.transform.DOMove(exportPickers[_coffeePositionNumber].position, 1);
            exportProduct.transform.parent = exportPickers[_coffeePositionNumber].transform;
        }

        IEnumerator WaitTurn(int time)
        {
            yield return new WaitForSeconds(time);
            PushExport();
        }
    }
}