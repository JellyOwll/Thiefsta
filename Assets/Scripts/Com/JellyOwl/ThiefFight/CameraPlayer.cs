///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 16/09/2019 17:45
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.PlayerObject;
using System.Collections.Generic;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight {
	public class CameraPlayer : MonoBehaviour {

        [SerializeField]
        protected int playerNumber;
        protected static List<Player> targets = new List<Player>();
        protected Vector3 velocity;
        [SerializeField]
        protected Vector3 offset;
        protected float timeSmooth = .5f;
	
		private void Start () {
			
		}
		
		private void Update () {
			
		}

        protected void LateUpdate()
        {
            targets = Player.playersList;
            for (int i = targets.Count- 1; i >= 0; i--)
            {
                if(targets[i].PlayerNumber == playerNumber)
                {
                    Vector3 lPosition = targets[i].transform.position;
                    transform.position = Vector3.SmoothDamp(transform.position, lPosition + offset , ref velocity, timeSmooth );
                    return;
                }
            }
        }
    }
}