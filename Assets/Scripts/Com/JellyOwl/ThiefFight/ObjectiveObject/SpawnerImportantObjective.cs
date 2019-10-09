///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 17/09/2019 10:43
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.JellyOwl.ThiefFight.ObjectiveObject {
	public class SpawnerImportantObjective : MonoBehaviour {

        protected GameObject importantObjective;
	
		private void Start () {
            importantObjective = Resources.Load<GameObject>("Prefab/Objective/ImportantPaint");

        }

        public void SpawnImportantObjective()
        {
            GameObject lObjective = Instantiate(importantObjective);
            lObjective.transform.position = transform.position;
            Objective.currentObjective = lObjective.GetComponent<Objective>();
            Objective.currentObjective.isObjective = true;
            Debug.Log(lObjective.transform.position);
        }
		
		private void Update () {
			
		}
	}
}