///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 16/09/2019 20:23
///-----------------------------------------------------------------

using UnityEngine;
using TMPro;
namespace Com.JellyOwl.ThiefFight.Menus {
	public class WinScreen : MonoBehaviour {
		private static WinScreen instance;
		public static WinScreen Instance { get { return instance; } }
        public TextMeshProUGUI playerLabelWin;
        public TextMeshProUGUI win;
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
            playerLabelWin.enabled = false;
            win.enabled = false;

        }

        public void CheckPlayerWin(string text)
        {
            playerLabelWin.enabled = true;
            win.enabled = true;
            playerLabelWin.text = text;
        }

		private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}