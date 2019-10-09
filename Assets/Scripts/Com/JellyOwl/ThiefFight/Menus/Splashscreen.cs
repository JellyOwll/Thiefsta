///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 20/09/2019 21:48
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Com.JellyOwl.ThiefFight.Menus {
	public class Splashscreen : MonoBehaviour {
		private static Splashscreen instance;
		public static Splashscreen Instance { get { return instance; } }
        protected Controller controller;
        [SerializeField]
        protected VideoPlayer video;

        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        private void Start()
        {
            video.Play();
            controller = new Controller();
        }


        void Update()
        {
            if (Input.GetKeyDown(controller.start))
            {
                video.Stop();
            }
        }

        private void LateUpdate()
        {
            if (!video.isPlaying)
            {
                SceneManager.LoadScene("Menu");
            }
        }

        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}