///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 12/09/2019 21:13
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Com.JellyOwl.ThiefFight.Managers;
using System.Collections;
using Com.JellyOwl.ThiefFight.Collectibles;

namespace Com.JellyOwl.ThiefFight.ObjectiveObject {

    public class Objective : Collectible {
        public static List<GameObject> objectives = new List<GameObject>();
        public static Objective currentObjective;
        protected static Objective instance;
        public CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        protected GameObject effect;

        override protected void Start () {
            base.Start();
            objectives.Add(gameObject);
            virtualCamera.enabled = false;
            instance = this;

        }
		
		private void Update () {
			
		}

        public static void ChooseObjective()
        {
            int lIndex = Random.Range(0, Objective.objectives.Count);
            currentObjective = Objective.objectives[lIndex].GetComponent<Objective>();
            currentObjective.CheckIsObjective(true);
            instance.StartCoroutine(CameraSwitch());
        }

        static public IEnumerator CameraSwitch()
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.virtualCameraPlayer.enabled = false;
            currentObjective.virtualCamera.enabled = true;
            yield return new WaitForSeconds(1.5f);
            currentObjective.virtualCamera.enabled = false;
            GameManager.Instance.virtualCameraPlayer.enabled = true;
        }

        public void CheckIsObjective(bool isGoal)
        {
            isObjective = isGoal;
            if (isObjective)
            {
                score += 10;
                effect.GetComponent<ParticleSystem>().Play();
            } else
            {
                score = 5;
                effect.GetComponent<ParticleSystem>().Stop();

            }

        }

        virtual protected void OnDestroy()
        {
            objectives.Remove(gameObject);
            if (isObjective)
            {
                ChooseObjective();
            }
        }
    }
}