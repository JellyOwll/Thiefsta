///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 10/10/2019 23:47
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using Com.JellyOwl.ThiefFight.ObjectiveObject;
using UnityEngine;
using UnityEngine.UI;

namespace Com.JellyOwl.ThiefFight.PlayerObject {

    public class ObjectiveArrow : MonoBehaviour {

        [SerializeField]
        protected Transform objectiveArrowPivot;
        [SerializeField]
        protected Image arrowGraphic;
        protected bool followtruck;
        [SerializeField]
        protected float objectiveArrowPlayer;


        private void Start() {
            objectiveArrowPlayer = GetComponentInParent<Player>().PlayerNumber;
        }

        private void Update() {
            
        }
        public bool Followtruck { 
            get => followtruck;
            set => followtruck = value;
        }

        public void CheckArrow()
        {
            if (GameManager.Instance.mode == BestOfThieves.BestOfThieves.ToString())
            {
                arrowGraphic.enabled = true;
                Debug.Log("Best");
            } else
            {
                arrowGraphic.enabled = false;
                Debug.Log("No");
                
            }
        }

        private void LateUpdate()
        {
            if (Objective.currentObjective is null)
            {
                arrowGraphic.enabled = false;

            }
            else
            {
                arrowGraphic.enabled = true;
                if(Followtruck)
                {
                    for (int i = PlayerZone.playerZoneList.Count - 1; i >= 0; i--)
                    {
                        if (PlayerZone.playerZoneList[i].playerNumber == objectiveArrowPlayer)
                        {
                            objectiveArrowPivot.LookAt(PlayerZone.playerZoneList[i].transform.position);
                            Debug.Log("PlayerZone: " + PlayerZone.playerZoneList[i].transform.position);
                            Debug.Log("Objective: " + Objective.currentObjective.transform.position);
                            break;
                        }
                    }
                } else
                {
                    objectiveArrowPivot.LookAt(Objective.currentObjective.transform.position);

                }
            }
        }
    }
}