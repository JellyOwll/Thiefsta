///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 00:38
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using Pixelplacement;
//using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.Menus {
	public class CouchParty : UI {
		private static CouchParty instance;
		public static CouchParty Instance { get { return instance; } }

        [SerializeField]
        protected Button twoPlayers;
        [SerializeField]
        protected Button threePlayers;
        [SerializeField]
        protected Button fourPlayer;
        [SerializeField]
        protected Button back;

        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        override protected void Start()
        {
            base.Start();
            eventSystem.SetSelectedGameObject(fourPlayer.gameObject);
        }

        private void Update()
        {
            if (eventSystem.currentSelectedGameObject is null)
            {
                eventSystem.SetSelectedGameObject(fourPlayer.gameObject);
            }
        }

        public void ThreePlayer()
        {
            GameManager.Instance.NumberOfPlayerMax = 3;
            LeftTransition(Mode);
            MenuManager.Instance.GoToCouchPartyMode();

        }

        public void TwoPlayer()
        {
            
            GameManager.Instance.NumberOfPlayerMax = 2;
            LeftTransition(Mode);
            MenuManager.Instance.GoToCouchPartyMode();

        }

        private void Mode()
        {
            Destroy(gameObject);
        }

        public void FourPlayer()
        {
            GameManager.Instance.NumberOfPlayerMax = 4;
            LeftTransition(Mode);
            Mode();
            MenuManager.Instance.GoToCouchPartyMode();

        }

        public void Back()
        {
            TransitionManager.Instance.MenuTransition(BackMenu);

        }

        public void BackMenu()
        {
            Destroy(gameObject);
            MenuManager.Instance.GoToMenu();
        }

        public void LeftTransition(Action action)
        {
            float delay = 0;
            Tween.AnchoredPosition(GetComponent<RectTransform>(), Vector3.right * -GetComponent<RectTransform>().rect.width, 0.7f, delay, Tween.EaseInOutBack, Tween.LoopType.None, null, () => action());
        }

        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}