///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/09/2019 14:32
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Collectibles;
using Com.JellyOwl.ThiefFight.Managers;
using Com.JellyOwl.ThiefFight.Menus;
using Com.JellyOwl.ThiefFight.ObjectiveObject;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.PlayerObject {
	public class PlayerZone : MonoBehaviour {

        [SerializeField]
        public int playerNumber;

        [SerializeField]
        protected ParticleSystem particleExplosion;
        public static List<PlayerZone> playerZoneList = new List<PlayerZone>();

        private void Start () {
            playerZoneList.Add(this);
		}
		
		private void Update () {

		}

        static void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickeable"))
            {
                ControllerManager.Instance.RumbleController(playerNumber, 1f);
                StartCoroutine((DestroyObjective(other.gameObject)));
            }
        }

        private IEnumerator DestroyObjective(GameObject objective)
        {
            objective.GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            if (!(objective.GetComponent<Objective>() is null))
            {
                if (objective.GetComponent<Objective>().isObjective)
                {
                    GameManager.Instance.SetSlowMotion();
                }
            }
            particleExplosion.Play();
            GameManager.Instance.IncrementScore(playerNumber, objective.GetComponent<Collectible>().score);
            Destroy(objective);
        }

        protected void OnDestroy()
        {
            playerZoneList.Remove(this);
        }
    }
}