///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 11:08
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.JellyOwl.ThiefFight.Managers {
	public class LevelManager : MonoBehaviour {
		private static LevelManager instance;
		public static LevelManager Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
			
		}

        public void GoToLevel(string name)
        {
            SceneManager.LoadScene(name);
        }

        public bool CheckActiveLevel(string name)
        {
            if(SceneManager.GetActiveScene().name == name)
            {
                return true;
            }
            return false;
        }
		
		private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}