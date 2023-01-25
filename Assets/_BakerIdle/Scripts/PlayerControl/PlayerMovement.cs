using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace StatueGames.BakeryIdle
{
    public class PlayerMovement : MonoBehaviour
    {
        private DynamicJoystick dynamicJoystick;
        public float speed;
        public float turnSpeed;
        public Transform bag;
        public int playerCapacity = 3;
        public int playerEmptyCapacity;
        public List<Item> stack = new List<Item>();

        public Animator playerAnimation;
        
        private float horizontal;
        private float vertical;

        public static PlayerMovement Instance;

        private void Awake()
        {
            Instance = this;
            playerEmptyCapacity = playerCapacity;
            
        }

        void Start()
        {
            dynamicJoystick = DynamicJoystick.Instance;
        }
        void Update()
        {
            if (Input.GetMouseButton(0)) 
            {
                PlayerJoystickMovement();
                playerAnimation.SetBool("isRunning", true);
                playerAnimation.SetBool("CarryRun", false);
                playerAnimation.SetBool("CarryIdle", false);
            }
            else
            {
                playerAnimation.SetBool("isRunning",false);
                playerAnimation.SetBool("CarryRun", false);
                playerAnimation.SetBool("CarryIdle", false);
            }


            if (Input.GetMouseButton(0) && bag.childCount != 0) 
            {
                playerAnimation.SetBool("CarryRun", true);
                playerAnimation.SetBool("CarryIdle", false);
            }

            else if (bag.childCount != 0) 
            {
                playerAnimation.SetBool("CarryIdle", true);
                playerAnimation.SetBool("CarryRun", false);
            }

            
        }

        public void PlayerJoystickMovement()
        {
            horizontal = dynamicJoystick.Horizontal;
            vertical = dynamicJoystick.Vertical;
            Vector3 addedPosition =
                new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
            gameObject.transform.position += addedPosition;
            
            Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                turnSpeed * Time.deltaTime);
            
        }
        public void PutItemInToStack(Item item)
        {
            playerEmptyCapacity--;
            stack.Add(item);
            item.transform.parent = bag.transform;
            // ReSharper disable once Unity.InefficientPropertyAccess
            item.transform.localRotation = Quaternion.identity;
            RepositionStack();
            
        }

        public Item GetItemFromStack(ItemType getItem)
        {
            if (stack.Count <= 0)
            {
                return null;
            }

            for (int i = 0; i < stack.Count; i++)
            {
                
                if (getItem == stack[i].itemType)
                {
                    playerEmptyCapacity++;
                    Item temp = stack[i];
                    stack.Remove(temp);
                    RepositionStack();
                    return temp;
                }
            }

            return null;
        }

        public void RepositionStack()
        {
            for (int i = 0; i < stack.Count; i++)
            {
                //stack[i].transform.position = bag.position;
                stack[i].transform.DOKill();
                stack[i].transform.position = bag.position + Vector3.up * (i * 0.5f);
                //stack[i].transform.DOMove(bag.position + Vector3.up * (i* 0.5f),0.5f);
            }
           
        }
    }
}