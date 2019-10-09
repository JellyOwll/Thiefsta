///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 30/09/2019 17:03
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.Managers;
using Com.JellyOwl.ThiefFight.Menus;
using Com.JellyOwl.ThiefFight.PlayerObject;
using System.Collections;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.Collectibles {
	public class Explosion : MonoBehaviour {
        [SerializeField]
        protected float explostionDesactivationTime = 0.1f;
        protected int lastPlayer;
		private void Start () {
            StartCoroutine(ExplosionForce());
		}

        protected IEnumerator ExplosionForce()
        {
            yield return new WaitForSeconds(explostionDesactivationTime);
            Destroy(this);
        }

        public void CheckPlayer(int lastPlayer)
        {
            this.lastPlayer = lastPlayer;
            Debug.Log("check " + lastPlayer);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.GetComponent<Rigidbody>().Equals(null))
            {
                other?.GetComponent<Rigidbody>()?.AddExplosionForce(500, transform.position, 30);
                if (!(other.GetComponent<Player>() is null))
                {
                    if (!other.GetComponent<Player>().isKilled)
                    {
                        other?.GetComponent<Player>()?.Killed();
                        if (other.GetComponent<Player>().PlayerNumber == lastPlayer)
                        {

                            switch (lastPlayer)
                            {
                                case 1:
                                    GameManager.Instance.scoreP1--;
                                    break;
                                case 2:
                                    GameManager.Instance.scoreP2--;
                                    break;
                                case 3:
                                    GameManager.Instance.scoreP3--;
                                    break;
                                case 4:
                                    GameManager.Instance.scoreP4--;
                                    break;
                            }
                        }
                        else
                        {
                            switch (lastPlayer)
                            {
                                case 1:
                                    GameManager.Instance.scoreP1++;
                                    break;
                                case 2:
                                    GameManager.Instance.scoreP2++;
                                    break;
                                case 3:
                                    GameManager.Instance.scoreP3++;
                                    break;
                                case 4:
                                    GameManager.Instance.scoreP4++;
                                    break;
                            }
                        }
                        HUD.Instance.updateScore();
                        Debug.Log(lastPlayer);
                    }
                }
                else if (!(other.GetComponent<ExplosiveCollectible>() is null))
                {

                    other?.GetComponent<ExplosiveCollectible>()?.Explode(lastPlayer);
                }
            }
        }
    }
}