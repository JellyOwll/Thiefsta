///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 01/10/2019 16:06
///-----------------------------------------------------------------


using UnityEngine;

namespace Com.JellyOwl.ThiefFight.Collectibles {
    public class SpawnerSpecialObject : MonoBehaviour {

        [SerializeField]
        protected float timeMax;
        [SerializeField]
        protected float maxSpecialObject;
        protected float count;

		private void Update () {
            count -= Time.deltaTime;
            if (count <= 0)
                {
                SpawnObject();
                count = timeMax;
            }
            
		}

        private void SpawnObject()
        {
            float lIndex = Mathf.Round(Random.Range(0f, maxSpecialObject));
            GameObject lObject = Instantiate(Resources.Load<GameObject>("Prefab/SpecialObject/" + lIndex ));
            lObject.transform.position = transform.position;
        }
    }
}