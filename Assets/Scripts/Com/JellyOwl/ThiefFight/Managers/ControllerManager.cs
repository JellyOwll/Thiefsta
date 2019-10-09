///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 04/10/2019 16:04
///-----------------------------------------------------------------

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Com.JellyOwl.ThiefFight.Managers {
	public class ControllerManager : MonoBehaviour {
		private static ControllerManager instance;
		public static ControllerManager Instance { get { return instance; } }
		
		private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}
		
		private void Start () {
			
		}

        public void RumbleController(int player)
        {
            StartCoroutine(Rumble(player));

        }

        public void RumbleController(int player, float time)
        {
            StartCoroutine(Rumble(player, time));

        }
        protected IEnumerator Rumble(int player)
        {

            Gamepad.all[player - 1]?.ResumeHaptics();
            Gamepad.all[player - 1]?.SetMotorSpeeds(10, 10);
            yield return new WaitForSecondsRealtime(0.1f);
            Gamepad.all[player - 1]?.PauseHaptics();
        }
        protected IEnumerator Rumble(int player, float time)
        {
            Gamepad.all[player - 1]?.ResumeHaptics();
            Gamepad.all[player - 1]?.SetMotorSpeeds(10, 10);
            yield return new WaitForSecondsRealtime(time);
            Gamepad.all[player - 1]?.PauseHaptics();
        }

        private void Update () {
			
		}
		
		private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}