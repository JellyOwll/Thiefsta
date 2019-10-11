///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/09/2019 10:52
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.AI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Com.JellyOwl.ThiefFight.Enemies {
	public class Enemy : Ai {
        private int destPoint = 0;
        protected NavMeshAgent agent;

        public List<GameObject> waypoints;

        override protected void Start () {
            base.Start();
            agent = GetComponent<NavMeshAgent>();
            selectCheckpointGoal();
            setModePatrol();
        }

        protected override void doActionPatrol()
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f) { 
                selectCheckpointGoal();
            }

        }

        protected void selectCheckpointGoal() {
            if (waypoints.Count == 0) return;
            agent.destination = waypoints[destPoint].transform.position;
            destPoint = (destPoint + 1) % waypoints.Count;
        }
	}
}