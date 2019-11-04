///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 13:12
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using Com.JellyOwl.ThiefFight.Menus;
using Com.JellyOwl.ThiefFight.PlayerObject;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.Collectibles {
	public class Collectible : MonoBehaviour {
        
        public bool isThrow = false;
        public bool isObjective = false;
        [HideInInspector]
        public int score = 0;
        public int LastPlayer;
        protected Rigidbody rb;
        protected Outline outline;
        virtual protected void Start () {
            rb = GetComponent<Rigidbody>();
            outline = GetComponent<Outline>();
            outline.enabled = false;
            Player.OnDrop += Player_OnDrop;
            Player.OnPick += Player_OnPick;
            Player.OnThrow += Player_OnThrow;
		}

        protected virtual void Player_OnThrow(Player sender)
        {
            if (LastPlayer == sender.PlayerNumber)
            {
                sender.PickedObject.Remove(this);
                rb.isKinematic = false;
                GetComponent<Collider>().enabled = true;
                transform.position = sender.launch.transform.position;
                isThrow = true;
                rb.AddTorque(sender.transform.right * sender.VerticalForce, ForceMode.Impulse);
                rb.AddForce(sender.transform.forward * sender.HorizontalForce, ForceMode.Impulse);
                rb.AddForce(sender.transform.up * sender.VerticalForce, ForceMode.Impulse);
            }
        }

        protected virtual void Player_OnPick(Player sender)
        {
            if(sender.CollectableObject.IndexOf(this) == 0)
            {
                LastPlayer = sender.PlayerNumber;
                sender.PickedObject.Add(this);
                rb.isKinematic = true;
                GetComponent<Collider>().enabled = false;
                outline.enabled = false;
                if (isObjective)
                {
                    sender.slowObjective = true;
                    sender.objectiveArrow.Followtruck = true;
                }
                sender.CollectableObject.Remove(this);
            }
        }

        virtual protected void Player_OnDrop(Player sender)
        {
            if(LastPlayer == sender.PlayerNumber)
            {
                sender.PickedObject.Remove(this);
                rb.isKinematic = false;
                GetComponent<Collider>().enabled = true;
            }
        }

        private void Update () {
			
		}

       virtual protected void OnCollisionEnter(Collision collision)
        {
            if (isThrow)
            {
                LastPlayer = 0;
            }
            if (collision.gameObject.CompareTag("Player"))
            {
                if (isThrow)
                {
                    if (GameManager.Instance.mode == DeathMatch.DeathMatch.ToString())
                    {
                        collision.gameObject.GetComponent<Player>().Killed();
                        if(LastPlayer == 1)
                        {
                            GameManager.Instance.scoreP1++;
                        }
                        else if (LastPlayer == 2)
                        {
                            GameManager.Instance.scoreP2++;
                        }
                        else if (LastPlayer == 3)
                        {
                            GameManager.Instance.scoreP3++;

                        }
                        else if (LastPlayer == 4) {
                            GameManager.Instance.scoreP4++;
                        }
                        HUD.Instance?.updateScore();
                    }
                    else
                    {
                        collision.gameObject.GetComponent<Player>().SetModeStun();
                    }
                }
                else
                {

                }
                
            } 
            isThrow = false;
        }
    }
}