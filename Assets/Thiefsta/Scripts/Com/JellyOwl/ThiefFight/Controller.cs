///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 13/09/2019 09:35
///-----------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.JellyOwl.ThiefFight {

    public class Controller :MonoBehaviour
    {
        public KeyCode a;
        public KeyCode b;
        public KeyCode x;
        public KeyCode y;
        public KeyCode start;
        public KeyCode select;
        public static string[] joystickList;

        protected string PS4 = "Wireless Controller";
        protected string XboxOne = "Controller (Xbox One For Windows)";

        public bool isPs4;
        public bool isXbox;

        public int PlayerNumber { get; }

        public Controller(int playerNumber)
        {
            PlayerNumber = playerNumber;
            GetInput(playerNumber);
        }
        public Controller()
        {
            GetInput();
        }

        public static void checkController()
        {
            joystickList = Input.GetJoystickNames();
        }

        private void GetInput(int playerNumber)
        {
            checkController();
            if (playerNumber == 1)
            {
                if (joystickList[playerNumber -1] == PS4)
                {
                    isPs4 = true;
                    Debug.Log("PS4");
                    a = KeyCode.Joystick1Button1;
                    b = KeyCode.Joystick1Button2;
                    x = KeyCode.Joystick1Button0;
                    y = KeyCode.Joystick1Button3;
                    start = KeyCode.Joystick1Button9;
                    select = KeyCode.Joystick1Button8;
                }
                else
                {
                    isXbox = true;
                    Debug.Log("Xbox One");
                    a = KeyCode.Joystick1Button0;
                    b = KeyCode.Joystick1Button1;
                    x = KeyCode.Joystick1Button2;
                    y = KeyCode.Joystick1Button3;
                    start = KeyCode.Joystick1Button7;
                    select = KeyCode.Joystick1Button6;
                }

            }
            else if (playerNumber == 2)
            {
                if (joystickList[playerNumber - 1] == PS4)
                {
                    isPs4 = true;
                    Debug.Log("PS4");
                    a = KeyCode.Joystick2Button1;
                    b = KeyCode.Joystick2Button2;
                    x = KeyCode.Joystick2Button0;
                    y = KeyCode.Joystick2Button3;
                    start = KeyCode.Joystick2Button9;
                    select = KeyCode.Joystick2Button8;
                }
                else
                {
                    isXbox = true;
                    Debug.Log("Xbox One");
                    a = KeyCode.Joystick2Button0;
                    b = KeyCode.Joystick2Button1;
                    x = KeyCode.Joystick2Button2;
                    y = KeyCode.Joystick2Button3;
                    start = KeyCode.Joystick2Button7;
                    select = KeyCode.Joystick2Button6;

                }
            }
            else if (playerNumber == 3)
            {
                if (joystickList[playerNumber - 1] == PS4)
                {
                    isPs4 = true;
                    Debug.Log("PS4");
                    a = KeyCode.Joystick3Button1;
                    b = KeyCode.Joystick3Button2;
                    x = KeyCode.Joystick3Button0;
                    y = KeyCode.Joystick3Button3;
                    start = KeyCode.Joystick3Button9;
                    select = KeyCode.Joystick3Button8;
                }
                else
                {
                    isXbox = true;
                    Debug.Log("Xbox One");
                    a = KeyCode.Joystick3Button0;
                    b = KeyCode.Joystick3Button1;
                    x = KeyCode.Joystick3Button2;
                    y = KeyCode.Joystick3Button3;
                    start = KeyCode.Joystick3Button7;
                    select = KeyCode.Joystick3Button6;

                }
            }
            else if (playerNumber == 4)
            {
                if (joystickList[playerNumber - 1] == PS4)
                {
                    isPs4 = true;
                    Debug.Log("PS4");
                    a = KeyCode.Joystick4Button1;
                    b = KeyCode.Joystick4Button2;
                    x = KeyCode.Joystick4Button0;
                    y = KeyCode.Joystick4Button3;
                    start = KeyCode.Joystick4Button9;
                    select = KeyCode.Joystick4Button8;
                }
                else
                {
                    isXbox = true;
                    Debug.Log("Xbox One");
                    a = KeyCode.Joystick4Button0;
                    b = KeyCode.Joystick4Button1;
                    x = KeyCode.Joystick4Button2;
                    y = KeyCode.Joystick4Button3;
                    start = KeyCode.Joystick4Button7;
                    select = KeyCode.Joystick4Button6;
                }
            }
        }

        private void GetInput()
        {
            checkController();
            foreach(string item in joystickList)
            {
                if(item == PS4)
                {
                    a = KeyCode.JoystickButton1;
                    b = KeyCode.JoystickButton2;
                    x = KeyCode.JoystickButton0;
                    y = KeyCode.JoystickButton3;
                    start = KeyCode.JoystickButton9;
                    select = KeyCode.JoystickButton8;
                } else
                {
                    a = KeyCode.JoystickButton0;
                    b = KeyCode.JoystickButton1;
                    x = KeyCode.JoystickButton2;
                    y = KeyCode.JoystickButton3;
                    start = KeyCode.JoystickButton7;
                    select = KeyCode.JoystickButton6;
                }
            }
        }
    }
}

