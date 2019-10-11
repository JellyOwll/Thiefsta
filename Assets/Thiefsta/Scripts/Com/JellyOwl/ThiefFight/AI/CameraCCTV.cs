///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 17/09/2019 17:45
///-----------------------------------------------------------------

using UnityEngine;

namespace Com.JellyOwl.ThiefFight.AI {
	public class CameraCCTV : Ai {

        public GameObject pivot;
        public GameObject door;
        public MeshRenderer wireLinkRenderer;
        public float timerDoor;
        override protected void Start() {
            base.Start();
            wireLinkRenderer.material = WireOpen;
            pivot.transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        private void Update () {
		}
        protected void LateUpdate()
        {
            if (isVisible)
            {
                pivot.transform.LookAt(viewPoint);
                timerDoor = 3;
                if (!door.activeSelf)
                {
                    Debug.Log("Ferme");
                    wireLinkRenderer.material = WireClose;
                    door.SetActive(true);
                }
            }
            else
            {
                if (door.activeSelf)
                {
                    timerDoor -= Time.deltaTime;
                    if (timerDoor <= 0)
                    {
                        wireLinkRenderer.material = WireOpen;
                        door.SetActive(false);
                        timerDoor = 3;
                    }
                }
            }
        }
    }
}