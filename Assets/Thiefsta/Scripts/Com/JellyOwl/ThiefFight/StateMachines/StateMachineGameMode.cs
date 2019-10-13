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
        protected abstract void DoModeVoid();

        protected void SetModeBestOfThief() => DoMode = DoModeBestOfThief;
        protected void SetModeDeathMatch() => DoMode = DoModeDeathMatch;
        protected void SetModeVoid() => DoMode = DoModeVoid;

        protected override void Update()
        {
            base.Update();
            DoMode();
        }

        
    }
}