///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 00:38
///-----------------------------------------------------------------

using System;
using Com.JellyOwl.ThiefFight.Managers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.Menus
{
    public class CouchPartyMode : UI
    {
        private static CouchPartyMode instance;
        public static CouchPartyMode Instance { get { return instance; } }

        [SerializeField]
        protected Button eliminationBtn;
        [SerializeField]
        protected Button thievesAndGuardsBtn;
        [SerializeField]
        protected Button bestOfThievesBtn;
        [SerializeField]
        protected Button DeathMatchBtn;
        [SerializeField]
        protected Button back;

        private void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        override protected void Start()
        {
            base.Start();
            DOTween.Init();
            eventSystem.SetSelectedGameObject(bestOfThievesBtn.gameObject);
            TransitionSpawn();
        }

        private void TransitionSpawn()
        {
            GetComponent<RectTransform>().anchoredPosition += new Vector2(GetComponent<RectTransform>().rect.width, 0);
            GetComponent<RectTransform>().DOAnchorPosX(0, 1).SetEase(Ease.InOutBack);
        }
        private void Update()
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(bestOfThievesBtn.gameObject);
            }
        }

        public void BestOfThieves()
        {
            GameManager.Instance.mode = "BestOfThieves";
            TransitionManager.Instance.MenuTransition(BestOfThiefTransition);
        }

        public void DeathMatch()
        {

            GameManager.Instance.mode = "DeathMatch";
            TransitionManager.Instance.MenuTransition(DeathMatchTransition);

        }

        public void BestOfThiefTransition()
        {
            Destroy(gameObject);
            MenuManager.Instance.GoToMap();
        }

        public void DeathMatchTransition()
        {
            Destroy(gameObject);
            MenuManager.Instance.GoToMapDeathMatch();
        }

        public void Back()
        {
            TransitionManager.Instance.MenuTransition(BackMenu);
        }

        public void BackMenu()
        {
            Destroy(gameObject);
            MenuManager.Instance.GoToCouchParty();
        }

        private void OnDestroy()
        {
            if (this == instance) instance = null;
        }
    }
}