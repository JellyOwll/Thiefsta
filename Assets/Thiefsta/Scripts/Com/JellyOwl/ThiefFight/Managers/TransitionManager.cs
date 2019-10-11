///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 05/10/2019 11:57
///-----------------------------------------------------------------

//using DG.Tweening;
using Com.JellyOwl.ThiefFight.PlayerObject;
using Pixelplacement;
using System;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.Managers {
	public class TransitionManager : MonoBehaviour {
		private static TransitionManager instance;
		public static TransitionManager Instance { get { return instance; } }

        [SerializeField]
        protected RectTransform bottomTransition;
        [SerializeField]
        protected RectTransform upTransition;

        protected float upY;
        protected float botY;
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        private void Start()
        {
            botY = bottomTransition.anchoredPosition.y;
            upY = upTransition.anchoredPosition.y;
        }

        public void MenuTransition()
        {
            float delay = 0;
            Tween.AnchoredPosition(upTransition, Vector3.zero, .75f, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.zero, .75f, delay, Tween.EaseOutStrong);
            delay += 1f;
            Tween.AnchoredPosition(upTransition, Vector3.up * upY, .75f, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.up * botY, .75f, delay, Tween.EaseOutStrong);
        }

        public void MenuTransition(Action firstOnComplete)
        {
            float delay = 0;
            Tween.AnchoredPosition(upTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            delay += 1f;
            Tween.AnchoredPosition(upTransition, Vector3.up * upY, 2, delay, Tween.EaseOutStrong, Tween.LoopType.None, firstOnComplete);
            Tween.AnchoredPosition(bottomTransition, Vector3.up * botY, 2, delay, Tween.EaseOutStrong);
        }

        public void MenuTransition(Action firstOnComplete, Action secondOnComplete)
        {
            float delay = 0;
            Tween.AnchoredPosition(upTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            delay += 2f;
            Tween.AnchoredPosition(upTransition, Vector3.up * upY, 2, delay, Tween.EaseOutStrong, Tween.LoopType.None, firstOnComplete);
            Tween.AnchoredPosition(bottomTransition, Vector3.up * botY, 2, delay, Tween.EaseOutStrong, Tween.LoopType.None, secondOnComplete);
        }


        public void TransitionWinner()
        {
            float delay = 0;
            Tween.AnchoredPosition(upTransition, Vector3.up * 150, 0.75f, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.down * 150, 0.75f, delay, Tween.EaseOutStrong);
            delay += 1f;
            Tween.AnchoredPosition(upTransition, Vector3.zero, .2f, delay, Tween.EaseInBack);
            Tween.AnchoredPosition(bottomTransition, Vector3.zero, .2f, delay, Tween.EaseInBack);
            delay += 1;
            Tween.AnchoredPosition(upTransition, Vector3.up * upY, .75f, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.up * botY, .75f, delay, Tween.EaseOutStrong);
        }

        public void TransitionWinner(Action firstOnComplete)
        {
            float delay = 0;
            Tween.AnchoredPosition(upTransition, Vector3.up * 150, 0.75f, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.down * 150, 0.75f, delay, Tween.EaseOutStrong);
            delay += 1f;
            Tween.AnchoredPosition(upTransition, Vector3.zero, .2f, delay, Tween.EaseInBack);
            Tween.AnchoredPosition(bottomTransition, Vector3.zero, .2f, delay, Tween.EaseInBack);
            delay += .5f;
            Tween.AnchoredPosition(upTransition, Vector3.up * upY, 2, delay, Tween.EaseOutStrong, Tween.LoopType.None, firstOnComplete);
            Tween.AnchoredPosition(bottomTransition, Vector3.up * botY, .75f, delay, Tween.EaseOutStrong);
        }

        protected void ActionVoid()
        {
            Debug.Log("sdjfdsnf");
        }

        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}