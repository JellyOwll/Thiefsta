///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/10/2019
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.StateMachine
{
	abstract public class StateMachineGameMode : StateMachine {
        protected Action DoMode;
        protected abstract void DoModeBestOfThief();
        protected abstract void DoModeDeathMatch();
        
        protected abstract void DoModeElimination();
        virtual protected void SetModeBestOfThief() => DoMode = DoModeBestOfThief;
        virtual protected void SetModeDeathMatch() => DoMode = DoModeDeathMatch;
        virtual protected void SetModeVoid() => DoMode = DoModeVoid;
        virtual protected void SetModeElimination() => DoMode = DoModeElimination;

        public override void Start()
        {
            base.Start();
            SetModeVoid();
        }

        protected override void Update()
        {
            base.Update();
            DoMode();
        }

        protected void DoModeVoid()
        {

        }


    }
}