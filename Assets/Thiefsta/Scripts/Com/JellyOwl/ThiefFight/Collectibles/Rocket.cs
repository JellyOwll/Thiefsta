///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 01/10/2019 11:01
///-----------------------------------------------------------------

using System;
using Com.JellyOwl.ThiefFight.PlayerObject;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.Collectibles {
	public class Rocket : ExplosiveCollectible {

        protected Action DoAction;
        [Header("Movement")]
        protected float speed = 1000;

        protected override void Start()
        {
            base.Start();
            SetModeNormal();
            //SetModeLaunch();
        }

        private void SetModeNormal()
        {
            DoAction = DoActionNormal;
        }

        protected override void Player_OnThrow(Player sender)
        {
            sender.PickedObject.Remove(this);
            rb.isKinematic = false;
            GetComponent<Collider>().enabled = true;
            transform.position = sender.launch.transform.position;
            isThrow = true;
        }

        private void Update () {
            DoAction();
		}

        private void SetModeLaunch()
        {
            DoAction = DoActionLaunch;
        }

        protected void DoActionLaunch()
        {
            rb.useGravity = false;
            rb.velocity = transform.forward * speed * Time.deltaTime;

        }

        protected void DoActionNormal()
        {
            rb.isKinematic = false;
            if (isThrow)
            {
                SetModeLaunch();
            }
        }
	}
}