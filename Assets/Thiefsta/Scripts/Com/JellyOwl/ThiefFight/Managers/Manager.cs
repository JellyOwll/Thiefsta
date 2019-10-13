///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 01:46
///-----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.Managers {
	public class Manager : MonoBehaviour {
        static public List<GameObject> managers = new List<GameObject>();
        private void Awake(){
            managers.Add(this.gameObject);
            if (managers.Count > 1)
            {
                Destroy(this.gameObject);
                managers[0].GetComponentInChildren<MenuManager>().Start();
                managers[0].GetComponentInChildren<UiManager>().Start();
                managers[0].GetComponentInChildren<GameManager>().Start();
            }

            DontDestroyOnLoad(this.gameObject);
        }
	}
}