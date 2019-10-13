///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/10/2019
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.StateMachine
{
    public abstract class StateMachineManager : StateMachineGameMode
    {
        protected Action DoManager;

        abstract protected void ManagerWin();
        abstract protected void ManagerWaitRoom();
        
        virtual protected void SetManagerVoid() => DoManager = ManagerVoid;
        virtual protected void SetManagerWaitRoom() => DoManager = ManagerWaitRoom;
        virtual protected void SetManagerWin() => DoManager = ManagerWin;

        public override void Start()
        {
            base.Start();
            SetManagerVoid();
        }

        protected override void Update()
        {
            base.Update();
            DoManager();
        }

        protected void ManagerVoid()
        {

        }

    }
}