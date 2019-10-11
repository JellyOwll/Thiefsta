///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/09/2019 19:36
///-----------------------------------------------------------------

using System;
using Com.JellyOwl.ThiefFight.Menus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.JellyOwl.ThiefFight.Managers {
	public class MenuManager : MonoBehaviour {
        private const string Path = "Prefab/Menus/";
        private static MenuManager instance;
		public static MenuManager Instance { get { return instance; } }

        protected GameObject menu;
        protected GameObject couchParty;
        protected GameObject couchPartyMode;
        protected GameObject option;
        protected GameObject settings;
        protected GameObject audioSettings;
        protected GameObject screenSize;
        protected GameObject canvas;

        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		public void Start () {
            if (LevelManager.Instance.CheckActiveLevel("Menu"))
            {
                menu = Resources.Load<GameObject>(Path + "Menu");
                option = Resources.Load<GameObject>(Path+"Option");
                couchParty = Resources.Load<GameObject>(Path + "CouchParty");
                couchPartyMode = Resources.Load<GameObject>(Path + "CouchPartyMode");
                audioSettings = Resources.Load<GameObject>(Path + "AudioSettings");
                settings = Resources.Load<GameObject>(Path + "Settings");
                screenSize = Resources.Load<GameObject>(Path + "ScreenSize");
            }
            canvas = GameObject.FindGameObjectWithTag("Canvas");
        }

        private void Update () {
			
		}

        public void Quit()
        {
            #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        internal void GoToMapDeathMatch()
        {
            LevelManager.Instance.GoToLevel("Lab");
        }

        public void GoToOption()
        {
            GameObject lMenu = Instantiate(option, canvas.transform);
        }

        public void GoToCouchPartyMode()
        {
            GameObject lMenu = Instantiate(couchPartyMode, canvas.transform);
        }

        public void GoToSettings()
        {
            GameObject lMenu = Instantiate(settings, canvas.transform);

        }

        public void GoToCouchParty()
        {
            GameObject lMenu = Instantiate(couchParty, canvas.transform);

        }
        public void GoToMenu()
        {
            GameObject lMenu = Instantiate(menu, canvas.transform);
            Debug.Log(lMenu);
        }

        public void GoToMap()
        {
           LevelManager.Instance.GoToLevel("WaitRoom");
        }

		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}