///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 05/10/2019 11:57
///-----------------------------------------------------------------

//using DG.Tweening;
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
            /*Sequence mySequence = DOTween.Sequence();
            mySequence
                .Append(upTransition.DOAnchorPosY(0, 1)
                    .SetEase(Ease.OutFlash))
                .Insert(0, bottomTransition.DOAnchorPosY(0, 1)
                    .SetEase(Ease.OutFlash))
                .AppendInterval(0.3f)
                .Append(upTransition.DOAnchorPosY(upY, 1)
                    .SetEase(Ease.InFlash))
                .Insert(1.3f, bottomTransition.DOAnchorPosY(botY, 1)
                    .SetEase(Ease.InFlash));
                    */

        }

        public void MenuTransition(Action firstOnComplete)
        {
            float delay = 0;
            Tween.AnchoredPosition(upTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            delay += 1f;
            Tween.AnchoredPosition(upTransition, Vector3.up * upY, 2, delay, Tween.EaseOutStrong, Tween.LoopType.None, firstOnComplete);
            Tween.AnchoredPosition(bottomTransition, Vector3.up * botY, 2, delay, Tween.EaseOutStrong);
            /*Sequence mySequence = DOTween.Sequence();
            mySequence
                .Append(upTransition.DOAnchorPosY(0, 1)
                    .SetEase(Ease.OutFlash))
                .Insert(0, bottomTransition.DOAnchorPosY(0, 1)
                    .OnComplete(() => OnComplete(firstOnComplete))
                    .SetEase(Ease.OutFlash))
                .AppendInterval(0.3f)
                .Append(upTransition.DOAnchorPosY(upY, 1)
                    .SetEase(Ease.InFlash))
                .Insert(1.3f, bottomTransition.DOAnchorPosY(botY, 1)
                    .SetEase(Ease.InFlash));*/
        }

        public void MenuTransition(Action firstOnComplete, Action secondOnComplete)
        {
            float delay = 0;
            Tween.AnchoredPosition(upTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            Tween.AnchoredPosition(bottomTransition, Vector3.zero, 2, delay, Tween.EaseOutStrong);
            delay += 2f;
            Tween.AnchoredPosition(upTransition, Vector3.up * upY, 2, delay, Tween.EaseOutStrong, Tween.LoopType.None, firstOnComplete);
            Tween.AnchoredPosition(bottomTransition, Vector3.up * botY, 2, delay, Tween.EaseOutStrong, Tween.LoopType.None, secondOnComplete);
            /*Sequence mySequence = DOTween.Sequence();
            mySequence
                .Append(upTransition.DOAnchorPosY(0, 1)
                    .SetEase(Ease.OutFlash))
                .Insert(0, bottomTransition.DOAnchorPosY(0, 1)
                    .OnComplete(() => OnComplete(firstOnComplete))
                    .SetEase(Ease.OutFlash))
                .AppendInterval(0.3f)
                .Append(upTransition.DOAnchorPosY(upY, 1)
                    .SetEase(Ease.InFlash))
                .Insert(1.3f, bottomTransition.DOAnchorPosY(botY, 1)
                    .OnComplete(() => OnComplete(secondOnComplete))
                    .SetEase(Ease.InFlash));*/
        }

        protected void OnComplete(Action action)
        {
            action();
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