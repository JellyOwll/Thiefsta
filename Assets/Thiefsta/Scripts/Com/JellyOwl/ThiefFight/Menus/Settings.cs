///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 04/10/2019 10:40
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Com.JellyOwl.ThiefFight.Menus {
	public class Settings : UI {
		private static Settings instance;
		public static Settings Instance { get { return instance; } }

        [SerializeField]
        TMP_Dropdown dropdownResolution;
        [SerializeField]
        Toggle toggleFullscreen;
        [SerializeField]
        TMP_Dropdown dropdownGraphics;
        protected bool isFullscreen = true;

        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}

            instance = this;
		}

        protected override void Start()
        {
            base.Start();
            dropdownResolution.onValueChanged.AddListener(delegate
            {
                OnResolutionChange(dropdownResolution);
            });
            toggleFullscreen.onValueChanged.AddListener(delegate
            {
                OnFullScreenChanged(toggleFullscreen);
            });
            dropdownGraphics.onValueChanged.AddListener(delegate
            {
                OnQualityChange(dropdownGraphics);
            });
        }

        public void OnFullScreenChanged(Toggle change)
        {
            Screen.fullScreen = isFullscreen = change.isOn;
            
        }

        public void OnQualityChange(TMP_Dropdown change)
        {
            QualitySettings.SetQualityLevel(change.value);
        }

        public void OnResolutionChange(TMP_Dropdown change)
        {
            switch (change.value)
            {
                case 0:
                    Screen.SetResolution(1024, 768, isFullscreen);
                    break;
                case 1:
                    Screen.SetResolution(1152, 864, isFullscreen);
                    break;
                case 2:
                    Screen.SetResolution(1280, 720, isFullscreen);
                    break;
                case 3:
                    Screen.SetResolution(1280, 800, isFullscreen);
                    break;
                case 4:
                    Screen.SetResolution(1280, 960, isFullscreen);
                    break;
                case 5:
                    Screen.SetResolution(1280, 1024, isFullscreen);
                    break;
                case 6:
                    Screen.SetResolution(1360, 768, isFullscreen);
                    break;
                case 7:
                    Screen.SetResolution(1366, 768, isFullscreen);
                    break;
                case 8:
                    Screen.SetResolution(1400, 1050, isFullscreen);
                    break;
                case 9:
                    Screen.SetResolution(1440, 900, isFullscreen);
                    break;
                case 10:
                    Screen.SetResolution(1600, 900, isFullscreen);
                    break;
                case 11:
                    Screen.SetResolution(1680, 1050, isFullscreen);
                    break;
                case 12:
                    Screen.SetResolution(1920, 1080, isFullscreen);
                    break;
            }
        }

		private void Update () {
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(dropdownResolution.gameObject);
            }
        }
		
		private void OnDestroy(){
            if (this == instance) instance = null;
		}
	}
}