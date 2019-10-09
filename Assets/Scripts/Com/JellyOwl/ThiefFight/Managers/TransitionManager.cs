///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 05/10/2019 11:57
///-----------------------------------------------------------------

using DG.Tweening;
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
            DOTween.Init();
            botY = bottomTransition.anchoredPosition.y;
            upY = upTransition.anchoredPosition.y;
        }

        public void MenuTransition()
        {
            Sequence mySequence = DOTween.Sequence();
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


        }

        public void MenuTransition(Action firstOnComplete)
        {
            Sequence mySequence = DOTween.Sequence();
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
                    .SetEase(Ease.InFlash));
        }

        public void MenuTransition(Action firstOnComplete, Action secondOnComplete)
        {
            Sequence mySequence = DOTween.Sequence();
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
                    .SetEase(Ease.InFlash));
        }

        protected void OnComplete(Action action)
        {
            action();
        }

        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}