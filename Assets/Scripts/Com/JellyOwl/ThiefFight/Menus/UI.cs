///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 00:28
///-----------------------------------------------------------------

using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.JellyOwl.ThiefFight.Menus {
	public class UI : MonoBehaviour {

        [SerializeField]
        protected EventSystem eventSystem;
        protected StandaloneInputModule inputModule;

		virtual protected void Start () {
            AddController();
            
        }

        protected void AddController()
        {
            Controller lController = new Controller(1);
            eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
            inputModule = eventSystem.GetComponent<StandaloneInputModule>();
            if (lController.isPs4)
            {
                inputModule.submitButton = "PS4Use";
                inputModule.cancelButton = "PS4Cancel";

            }
            else if (lController.isXbox)
            {
                inputModule.submitButton = "XboxUse";
                inputModule.cancelButton = "XboxCancel";
            }
        }

        private void Update () {
			
		}

        protected void ResetEventSystem()
        {
            eventSystem.SetSelectedGameObject(null);
        }
	}
}