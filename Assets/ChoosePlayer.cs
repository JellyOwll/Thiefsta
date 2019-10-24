///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 23/10/2019 16:48
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.Menus
{

    public enum PlayerOutfit
    {
        Outfit1 = 1,
        Outfit2 = 2,
        Outfit3 = 3,
        Outfit4 = 4,
    }

	public class ChoosePlayer : MonoBehaviour {

        [SerializeField]
        protected PlayerOutfit outfit;
        protected Button button;
		private void Start () {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
		}

        private void OnClick()
        {
            if(PlayerSelection.Instance.playerNumber == 1)
            {
                GameManager.Instance.CustomIndexPlayer1 = (int)outfit;
            }
            else if (PlayerSelection.Instance.playerNumber == 2)
            {
                GameManager.Instance.CustomIndexPlayer2 = (int)outfit;
            } else if (PlayerSelection.Instance.playerNumber == 3)
            {
                GameManager.Instance.CustomIndexPlayer3 = (int)outfit;
            } else if (PlayerSelection.Instance.playerNumber == 4)
            {
                GameManager.Instance.CustomIndexPlayer4 = (int)outfit;
            }
            button.interactable = false;
            PlayerSelection.Instance.NextPlayer();
        }

        private void Update () {
			
		}
	}
}