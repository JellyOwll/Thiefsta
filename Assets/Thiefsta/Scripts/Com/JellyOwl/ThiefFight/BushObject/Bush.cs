using Com.JellyOwl.ThiefFight.PlayerObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.BushObject { 
public class Bush : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other is CapsuleCollider)
            {
                if (other.CompareTag("Player"))
                {
                    Player lPlayer = other.gameObject.GetComponent<Player>();
                    lPlayer.slow = true;
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other is CapsuleCollider)
            {
                if (other.CompareTag("Player"))
                {
                    Player lPlayer = other.gameObject.GetComponent<Player>();
                    lPlayer.slow = true;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other is CapsuleCollider)
            {
                if (other.CompareTag("Player"))
                {
                    Player lPlayer = other.gameObject.GetComponent<Player>();
                    lPlayer.slow = false;
                }
            }
            
        }
    }
}