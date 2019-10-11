using Com.JellyOwl.ThiefFight.Collectibles;
using Com.JellyOwl.ThiefFight.PlayerObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.DestructibleObject {
    public class Glass : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pickeable"))
            {
                if (other.GetComponent<Collectible>().isThrow)
                {
                    Vector3 lPos = transform.position;
                    Quaternion lRot = transform.rotation;
                    Destroy(gameObject);
                    GameObject lBroke = Instantiate(Resources.Load<GameObject>("Prefab/Break/Window"));
                    lBroke.transform.position = lPos;
                    lBroke.transform.rotation = lRot;
                }
            }
        }
    }

}
