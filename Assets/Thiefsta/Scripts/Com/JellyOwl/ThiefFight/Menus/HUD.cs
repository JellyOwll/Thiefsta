///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 16/09/2019 10:18
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.Menus {
	public class HUD : MonoBehaviour {
		private static HUD instance;
		public static HUD Instance { get { return instance; } }
        public TextMeshProUGUI startText;
        public TextMeshProUGUI timeLeft;
        public TextMeshProUGUI scoreP1, scoreP2, scoreP3, scoreP4;
        public GameObject pause;
        public RawImage[] cams;
        public GameObject spawnHUD;
        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			instance = this;
		}
		
		private void Start () {
            if (!LevelManager.Instance.CheckActiveLevel("Menu"))
            {
                updateScore();
            }
        }

        public void checkNumberOfPlayerHUD(int playerNumber)
        {
            GameObject lObject = Instantiate(Resources.Load<GameObject>("Prefab/Players/HUD/" + playerNumber + "PlayerHud"), transform);
            lObject.transform.position = spawnHUD.transform.position;
            cams = lObject.GetComponentsInChildren<RawImage>();
            for (int i = 1; i <= playerNumber; i++)
            {
                if(i >= 1)
                {
                    scoreP1 = GameObject.FindGameObjectWithTag("ScoreP1").GetComponent<TextMeshProUGUI>();
                }
                if (i >= 2)
                {
                    scoreP2 = GameObject.FindGameObjectWithTag("ScoreP2").GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    continue;
                }
                if (i >= 3)
                {
                    scoreP3 = GameObject.FindGameObjectWithTag("ScoreP3").GetComponent<TextMeshProUGUI>();

                } else
                {
                    continue;
                }
                if (i >= 4)
                {
                    scoreP4 = GameObject.FindGameObjectWithTag("ScoreP4").GetComponent<TextMeshProUGUI>();

                } else
                {
                    continue;
                }
            }
        }

        public void setPause(bool isPause)
        {
            pause.SetActive(isPause);
        }

        public void setStartText(string text)
        {
            startText.text = text;
        }
        public void updateScore(){
            scoreP1.text = GameManager.Instance?.scoreP1.ToString() + " Pts";
            if(GameManager.Instance?.NumberOfPlayerMax >= 2)
            {
                scoreP2.text = GameManager.Instance?.scoreP2.ToString() + " Pts";
            }
            if (GameManager.Instance?.NumberOfPlayerMax >= 3)
            {
                scoreP3.text = GameManager.Instance?.scoreP3.ToString() + " Pts";
            }
            if (GameManager.Instance?.NumberOfPlayerMax >= 4)
            {
                scoreP4.text = GameManager.Instance?.scoreP4.ToString() + " Pts";
            }
        }

		private void Update () {
			
		}

        public void CamRemove()
        {
            foreach (RawImage item in cams)
            {
                item.enabled = false;
            }
        }

        public void CamSpawn()
        {
            foreach (RawImage item in cams)
            {
                item.enabled = true;
            }
        }
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}