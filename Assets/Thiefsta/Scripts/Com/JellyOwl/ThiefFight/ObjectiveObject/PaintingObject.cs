///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 12/09/2019 20:57
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.JellyOwl.ThiefFight.ObjectiveObject {
	public class PaintingObject : Objective {
        [SerializeField]
        protected GameObject imageSpawner;

		override protected void Start () {
            base.Start();
            rb.isKinematic = true;
            float lIndex = Mathf.Round(Random.Range(0f, 2f));
            Debug.Log(lIndex);
            GameObject image = Resources.Load<GameObject>("Sprites/" + lIndex);
            Debug.Log(image);
            if (!(image is null))
            {
                Debug.Log("Ok");
                image = Instantiate(image, imageSpawner.transform);
                image.transform.localPosition = Vector3.zero;
            }
		}
		
		private void Update () {
			
		}

        override protected void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
            if(collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Pickeable"))
            {
                rb.isKinematic = false;
            }
        }
    }
}