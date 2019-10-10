///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 10/10/2019 23:47
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.PlayerObject {

    public class ObjectiveArrow : MonoBehaviour {

        [SerializeField]
        protected Transform objectiveArrowPivot;
        private void Start() {

        }

        private void Update() {
            
        }

        public void CheckArrow()
        {
            if(GameManager.Instance.mode == BestOfThieves.BestOfThieves.ToString())
            {
                gameObject.SetActive(true);
                Debug.Log("Best");
            } else
            {
                gameObject.SetActive(false);
                Debug.Log("No");
            }
        }

        private void LateUpdate()
        {
            
        }
    }
}