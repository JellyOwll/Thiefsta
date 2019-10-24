///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 23/10/2019 15:08
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.Menus
{
	public class PlayerSelection : UI {
		private static PlayerSelection instance;
		public static PlayerSelection Instance { get { return instance; } }

        [SerializeField]
        protected Image backgroundImage;
        [SerializeField]
        protected Image bannerImagePlayer1, bannerImagePlayer2, bannerImagePlayer3, bannerImagePlayer4;
        [HideInInspector]
        public int playerNumber = 1;

        protected Button[] buttons;


		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        public void NextPlayer()
        {
            playerNumber++;
            BannerPlayerSelect[] bannerPlayers = GetComponentsInChildren<BannerPlayerSelect>();
            bannerPlayers[playerNumber - 2].IsFinish = true;

            if (playerNumber > GameManager.Instance.NumberOfPlayerMax)
            {
                Debug.Log("truc");
                MenuManager.Instance.GoToMap();
                return;
            }
            CheckPlayer();
            eventSystem.SetSelectedGameObject(buttons[0].gameObject);
        }

        protected override void Start()
        {
            base.Start();
            buttons = GetComponentsInChildren<Button>();
            eventSystem.SetSelectedGameObject(buttons[0].gameObject);
            CheckPlayer();
        }

        protected void CheckPlayer()
        {
            if(playerNumber == 1)
            {
                backgroundImage.color = bannerImagePlayer1.color;
            } else if (playerNumber == 2)
            {
                backgroundImage.color = bannerImagePlayer2.color;

            }
            else if (playerNumber == 3)
            {
                backgroundImage.color = bannerImagePlayer3.color;

            }
            else if (playerNumber == 4)
            {
                backgroundImage.color = bannerImagePlayer4.color;

            }
        }

		private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}