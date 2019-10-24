///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 23/10/2019 16:48
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerMenu
{
    Player1 = 1,
    Player2 = 2,
    Player3 = 3,
    Player4 = 4,
}
public enum PlayerMenuOutfit
{
    Outfit1 =1,
    Outfit2 =2,
    Outfit3 =3,
    Outfit4 =4
}


namespace Com.JellyOwl.ThiefFight.Menus
{
	public class BannerPlayerSelect : MonoBehaviour {

        [SerializeField]
        protected PlayerMenu player;
        [SerializeField]
        protected PlayerMenuOutfit outfitMenu;
        [SerializeField]
        protected Color colorPlayer;
        protected Image backgroundImage;
        [SerializeField]
        protected Image Finish;
        [SerializeField]
        protected Image outfit;
        [SerializeField]
        protected Sprite outfit1, outfit2, outfit3, outfit4;

        protected bool isFinish;

        public bool IsFinish
        {
            get => isFinish; 
            set
            {
                isFinish = value;
                UpdateFinish();
            }
        }

        protected PlayerMenuOutfit OutfitMenu
        {
            get => outfitMenu; 
            set
            {
                outfitMenu = value;
                CheckOutfit();
            }
        }

        private void CheckOutfit()
        {
            if(outfitMenu == (PlayerMenuOutfit)1)
            {
                outfit.sprite = outfit1;
            } else if (outfitMenu == (PlayerMenuOutfit)2)
            {
                outfit.sprite = outfit2;

            }
            else if (outfitMenu == (PlayerMenuOutfit)3)
            {
                outfit.sprite = outfit3;

            }
            else if (outfitMenu == (PlayerMenuOutfit)4)
            {
                outfit.sprite = outfit4;
            }
        }

        protected void Start()
        {
            CheckPlayer();
            CheckColor();
            UpdateFinish();
            outfit.sprite = null;

        }

        private void CheckPlayer()
        {
            if((int)player > GameManager.Instance.NumberOfPlayerMax)
            {
                gameObject.SetActive(false);
            }
        }

        protected void UpdateFinish()
        {
            if (IsFinish)
            {
                Finish.gameObject.SetActive(true);
            } else
            {
                Finish.gameObject.SetActive(false);
            }
            if(player == PlayerMenu.Player1)
            {
                OutfitMenu = (PlayerMenuOutfit)GameManager.Instance.CustomIndexPlayer1;

            }
            else if (player == PlayerMenu.Player2)
            {
                OutfitMenu = (PlayerMenuOutfit)GameManager.Instance.CustomIndexPlayer2;

            }
            else if (player == PlayerMenu.Player3)
            {
                OutfitMenu = (PlayerMenuOutfit)GameManager.Instance.CustomIndexPlayer3;

            }
            else if (player == PlayerMenu.Player4)
            {
                OutfitMenu = (PlayerMenuOutfit)GameManager.Instance.CustomIndexPlayer4;

            }

        }

        protected void CheckColor()
        {
            backgroundImage = GetComponent<Image>();
            backgroundImage.color = colorPlayer;
        }

        private void OnValidate()
        {
            CheckColor();
        }


	}
}