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
        protected abstract void DoActionNormal();
        virtual protected void SetActionNormal() => DoAction = DoActionNormal;
        virtual protected void SetActionVoid() => DoAction = DoActionVoid;
        protected virtual void Update() => DoAction();

        public virtual void Start()
        {
            SetActionVoid();
        }

        protected void DoActionVoid()
        {

        }
        
    }
}