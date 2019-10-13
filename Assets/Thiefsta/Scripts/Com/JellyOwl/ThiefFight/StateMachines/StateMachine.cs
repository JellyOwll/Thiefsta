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
            SetActionVoid();
        }

        protected virtual void Update() => DoAction();
        protected void SetActionNormal() => DoAction = DoActionNormal;
        protected void SetActionVoid() => DoAction = DoActionVoid;
    }
}