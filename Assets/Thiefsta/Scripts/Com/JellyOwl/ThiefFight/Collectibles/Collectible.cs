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
        public int score = 5;
        public int LastPlayer;
        protected Rigidbody rb;
        virtual protected void Start () {
            rb = GetComponent<Rigidbody>();
            GetComponent<Outline>().enabled = false;
		}
		
		private void Update () {
			
		}

       virtual protected void OnCollisionEnter(Collision collision)
        {
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
                        HUD.Instance.updateScore();
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