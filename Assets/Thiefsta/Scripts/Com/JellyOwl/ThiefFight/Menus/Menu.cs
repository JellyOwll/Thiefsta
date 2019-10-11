///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/09/2019 19:39
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using Pixelplacement;
//using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.Menus {
	public class Menu : UI {
		private static Menu instance;
		public static Menu Instance { get { return instance; } }
        protected int index;
        [SerializeField]
        protected Button couchParty;
        [SerializeField]
        protected Button Option;
        [SerializeField]
        protected Button Quit;

        static protected bool AlreadyAppeared;

		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		override protected void Start () {
            ButtonSpawn();
            base.Start();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void ButtonSpawn()
        {
            if (!AlreadyAppeared)
            {
                float delay = 0;
                AlreadyAppeared = true;
                RectTransform lCouchParty = couchParty.GetComponent<RectTransform>();
                RectTransform lOption = Option.GetComponent<RectTransform>();
                RectTransform lQuit = Quit.GetComponent<RectTransform>();
                float lCouchIniX = lCouchParty.anchoredPosition.x;
                float lOptionIniX = lOption.anchoredPosition.x;
                float lQuitIniX = lQuit.anchoredPosition.x;
                lCouchParty.anchoredPosition = new Vector2(-500, lCouchParty.anchoredPosition.y);
                lOption.anchoredPosition = new Vector2(-500, lOption.anchoredPosition.y);
                lQuit.anchoredPosition = new Vector2(-500, lQuit.anchoredPosition.y);
                Debug.Log("coucou");
                Tween.AnchoredPosition(lCouchParty, new Vector3(lCouchIniX, lCouchParty.anchoredPosition.y), .75f, delay, Tween.EaseOutBack);
                delay += .2f;
                Tween.AnchoredPosition(lOption, new Vector3(lOptionIniX, lOption.anchoredPosition.y), .75f, delay, Tween.EaseOutBack);
                delay += .2f;
                Tween.AnchoredPosition(lQuit, new Vector3(lOptionIniX, lQuit.anchoredPosition.y), .75f, delay, Tween.EaseOutBack);
            }
        }

        private void Update () {
            if(eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(couchParty.gameObject);
            }
		}
		
        public void QuitBtn()
        {
            TransitionManager.Instance.MenuTransition(MenuManager.Instance.Quit);
        }

        public void OptionBtn()
        {
            TransitionManager.Instance.MenuTransition(OptionMenu);
        }

        public void OptionMenu()
        {
            Debug.Log("Test");
            ResetEventSystem();
            Destroy(gameObject);
            MenuManager.Instance.GoToOption();
        }

        public void CouchPartyBtn()
        {
            TransitionManager.Instance.MenuTransition(CouchPartyMenu);

        }

        protected void CouchPartyMenu()
        {
            ResetEventSystem();
            Destroy(gameObject);
            MenuManager.Instance.GoToCouchParty();
        }

        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}