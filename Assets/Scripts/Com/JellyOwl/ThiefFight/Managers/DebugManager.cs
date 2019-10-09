///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 02:38
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.JellyOwl.ThiefFight.Managers {
    
	public class DebugManager : MonoBehaviour {
		private static DebugManager instance;

        static protected int playerNumber = 2;
		public static DebugManager Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
            playerNumber = 2;

        }
		
		private void Update () {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Menu");
                LevelManager.Instance.GoToLevel("Menu");
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("Restart");
                LevelManager.Instance.GoToLevel(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log("MuseumNormalMode");
                GameManager.Instance.mode = "BestOfThieves";
                GameManager.Instance.NumberOfPlayerMax = playerNumber;
                LevelManager.Instance.GoToLevel("MuseumNormal");
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                Debug.Log("LaboNormalMode");
                GameManager.Instance.mode = "BestOfThieves";
                GameManager.Instance.NumberOfPlayerMax = playerNumber;
                LevelManager.Instance.GoToLevel("Labo");
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Debug.Log("MuseumDeatchMatch");
                GameManager.Instance.mode = "DeathMatch";
                GameManager.Instance.NumberOfPlayerMax = playerNumber;
                LevelManager.Instance.GoToLevel("Lab");
            }

            

            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                playerNumber++;
                GameManager.Instance.NumberOfPlayerMax = playerNumber;
                Debug.Log("PlayerNumber : " + playerNumber);
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                playerNumber--;
                GameManager.Instance.NumberOfPlayerMax = playerNumber;
                Debug.Log("PlayerNumber : " + playerNumber);
            }

            if (playerNumber < 2)
            {
                playerNumber = 2;
            }

            if (playerNumber > 4)
            {
                playerNumber = 4;
            }
        }
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}