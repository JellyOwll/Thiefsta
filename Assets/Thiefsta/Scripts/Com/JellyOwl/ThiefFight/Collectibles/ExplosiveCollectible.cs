///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 30/09/2019 15:26
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.Collectibles {
	public class ExplosiveCollectible : Collectible {

        protected List<GameObject> explosionObject;

        protected override void OnCollisionEnter(Collision collision)
        {
            if (isThrow)
            {
                Explode(LastPlayer);
                LastPlayer = 0;
            }
        }

        virtual public void Explode(int pPlayer)
        {
            GameObject lExplosion;
            lExplosion = Instantiate(Resources.Load<GameObject>("Prefab/Explosion/BarrilExplosion"));
            lExplosion.GetComponent<Explosion>().CheckPlayer(pPlayer);
            lExplosion.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}