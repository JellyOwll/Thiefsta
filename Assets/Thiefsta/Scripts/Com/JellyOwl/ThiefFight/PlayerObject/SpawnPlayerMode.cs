///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 18/09/2019 10:20
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.JellyOwl.ThiefFight.PlayerObject {
	public class SpawnPlayerMode : MonoBehaviour {
        private const string PLAYERONE = "PLAYER1";
        private const string PLAYERTWO = "PLAYER2";
        private const string PLAYERTHREE = "PLAYER3";
        private const string PLAYERFOUR = "PLAYER4";
        private static SpawnPlayerMode instance;
		public static SpawnPlayerMode Instance { get { return instance; } }
        
        protected GameObject FourPlayer;
        protected GameObject ThreePlayer;
        protected GameObject TwoPlayer;
        protected GameObject OnePlayer;

        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
			
		}
		
        public void CheckPlayerNumber(int playerNumber)
        {
            GameObject lObject = Resources.Load<GameObject>("Prefab/Players/" + playerNumber + "PLAYER");
            lObject = Instantiate(lObject);
            lObject.transform.position = transform.position;
        }

		private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}