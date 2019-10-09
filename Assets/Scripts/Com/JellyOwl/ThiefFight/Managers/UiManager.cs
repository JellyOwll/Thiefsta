///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 10:43
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System;

namespace Com.JellyOwl.ThiefFight.Managers {
	public class UiManager : MonoBehaviour {
		private static UiManager instance;
		public static UiManager Instance { get { return instance; } }
        public TextMeshProUGUI timeLeft;
        protected int time;
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		public void Start () {
            if (LevelManager.Instance.CheckActiveLevel("WaitRoom"))
            {
                time = 10;
                timeLeft = GameObject.FindGameObjectWithTag("TimeLeft").GetComponent<TextMeshProUGUI>();
                timeLeft.text = time.ToString();
            } else if(!LevelManager.Instance.CheckActiveLevel("Menu"))
            {
                timeLeft = GameObject.FindGameObjectWithTag("TimeLeft").GetComponent<TextMeshProUGUI>();
            }
        }

        public void GoToLevel()
        {
            LevelManager.Instance.GoToLevel("MuseumTest");
        }


            
		private void Update () {
            if(!(timeLeft is null)){
                timeLeft.text = Mathf.Round(GameManager.Instance.timeMode).ToString();
            }
        }
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}