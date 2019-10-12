///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 04/10/2019 10:08
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.Menus {
	public class Option : UI {
		private static Option instance;
		public static Option Instance { get { return instance; } }

        [SerializeField]
        protected Button settings;
        [SerializeField]
        protected Button audioSettings;
        [SerializeField]
        protected Button screenArea;
        [SerializeField]
        protected Button clearSave;
        [SerializeField]
        protected Button back;

        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        public void OnSettings()
        {
            TransitionManager.Instance.TransitionToGame(Settings);
        }
        protected void Settings()
        {
            GoTo();
            MenuManager.Instance.GoToSettings();
        }

        public void OnAudioSettings()
        {
            ResetEventSystem();
            Destroy(gameObject);
        }

        public void OnScreenArea()
        {
            ResetEventSystem();
            Destroy(gameObject);
        }

        public void OnClearSave()
        {
        }

        public void OnBack()
        {
            TransitionManager.Instance.MenuTransition(Menu);
        }

        protected void Menu()
        {
            GoTo();
            MenuManager.Instance.GoToMenu();

        }

        protected void GoTo()
        {
            ResetEventSystem();
            Destroy(gameObject);
        }
        private void Update()
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(settings.gameObject);
            }
        }

        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}