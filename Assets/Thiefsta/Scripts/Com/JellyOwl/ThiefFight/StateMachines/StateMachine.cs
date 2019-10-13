///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/10/2019
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight.StateMachine {
    public abstract class StateMachine : MonoBehaviour
    {

        protected Action DoAction;
        protected abstract void DoActionVoid();
        protected abstract void DoActionNormal();

        protected virtual void Start()
        {
            SetModeVoid();
        }

        protected virtual void Update() => DoAction();

        protected void SetModeNormal() => DoAction = DoActionNormal;
        protected void SetModeVoid() => DoAction = DoActionVoid;
    }
}