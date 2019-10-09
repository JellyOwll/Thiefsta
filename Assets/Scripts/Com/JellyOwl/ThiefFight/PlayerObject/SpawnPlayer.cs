///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 02:08
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using System.Collections;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.PlayerObject {
	public class SpawnPlayer : MonoBehaviour {

        public int playerSpawnerNumber;

        [SerializeField]
        protected ParticleSystem particleSpawn;
        private void Start () {
            if(GameManager.Instance.NumberOfPlayerMax >= playerSpawnerNumber)
            {
                SpawnPlayers();
            }
        }
		
		private void Update () {
			
		}

        public void SpawnPlayers()
        {
            particleSpawn.Play();
            GameObject lPlayer = Instantiate(Resources.Load<GameObject>("Prefab/Players/" + playerSpawnerNumber));
            lPlayer.transform.position = transform.position;
            ControllerManager.Instance.RumbleController(playerSpawnerNumber, 0.1f);
        }

        public void SpawnAfterSeconds(float seconds)
        {
            StartCoroutine(Spawn(seconds));
        }

        protected IEnumerator Spawn(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            SpawnPlayers();
        }

	}
}