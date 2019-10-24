///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 10:29
///-----------------------------------------------------------------

using Cinemachine;
using Com.JellyOwl.ThiefFight.Menus;
using Com.JellyOwl.ThiefFight.ObjectiveObject;
using Com.JellyOwl.ThiefFight.PlayerObject;
using Com.JellyOwl.ThiefFight.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.JellyOwl.ThiefFight.Managers {

    enum PlayerNumber
    {
        PLAYER4,
        PLAYER3,
        PLAYER2,
        PLAYER1,
    } 
    enum BestOfThieves
    {
        BestOfThieves,
        TIMEMODE = 180
    }

    enum DeathMatch
    {
        DeathMatch,
        TIMEMODE = 180
    }
    enum WaitRoom
    {
        TIMEMODE = 10
    }

    enum Elimination
    {
        Elimination,
        TIMEMODE = 60
    }

    enum PolicemenAndThieves
    {
        PolicemenAndThieves,
        TIMEMODE = 180
    }


    public class GameManager : StateMachineManager {
        private static GameManager instance;
        public static GameManager Instance { get { return instance; } }

        [Header("All Mode")]
        protected Controller controller;
        public float timeMode;
        public int NumberOfPlayerMax;
        [HideInInspector]
        public string level = "MuseumNormal";

        [Space]
        [Header("Game")]
        protected Objective currentObjective;
        public String mode;
        public String winText;
        public CinemachineVirtualCamera virtualCameraPlayer;
        protected float lastTimeScale;
        public int scoreP1, scoreP2, scoreP3, scoreP4;
        [HideInInspector]
        public bool gameStart;
        protected bool pause = false;
        protected bool importantObjectiveSpawned;
        public int playerNumberWin = 0;

        [Space]
        [Header("HUB")]
        public string description;
        [SerializeField]
        protected SpawnPlayer PlayerSpawner1, PlayerSpawner2, PlayerSpawner3, PlayerSpawner4;
        [SerializeField]
        protected bool spawnedPlayer1, spawnedPlayer2, spawnedPlayer3, spawnedPlayer4;

        [Header("Custom Player")]
        [SerializeField]
        protected int customIndexPlayer1;
        [SerializeField]
        protected int customIndexPlayer2;
        [SerializeField]
        protected int customIndexPlayer3;
        [SerializeField]
        protected int customIndexPlayer4;


        public int CustomIndexPlayer1 { get => customIndexPlayer1; set => customIndexPlayer1 = value; }
        public int CustomIndexPlayer2 { get => customIndexPlayer2; set => customIndexPlayer2 = value; }
        public int CustomIndexPlayer3 { get => customIndexPlayer3; set => customIndexPlayer3 = value; }
        public int CustomIndexPlayer4 { get => customIndexPlayer4; set => customIndexPlayer4 = value; }


        private void Awake() {
            if (instance) {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        override public void Start()
        {
            base.Start();
            gameStart = true;
            controller = new Controller();
            if (LevelManager.Instance.CheckActiveLevel("WaitRoom"))
            {
                SetManagerWaitRoom();
            } else if (LevelManager.Instance.CheckActiveLevel("Menu"))
            {
                Reset();
            } else
            {
                CheckMode();
            }
        }

        private void Reset()
        {
            scoreP1 = scoreP2 = scoreP3 = scoreP4 = 0;
            mode = null;
            gameStart = false;
            pause = false;
            NumberOfPlayerMax = 0;
            Objective.currentObjective = null;
        }

        private void CheckMode()
        {
            scoreP1 = scoreP2 = scoreP3 = scoreP4 = 0;
            virtualCameraPlayer = GameObject.FindGameObjectWithTag("CMCamPlayer")?.GetComponent<CinemachineVirtualCamera>();

            if (mode == BestOfThieves.BestOfThieves.ToString())
            {
                timeMode = (int)BestOfThieves.TIMEMODE;
                SetModeBestOfThief();
                SetActionNormal();
                SetManagerVoid();
            } 
            else if (mode == Elimination.Elimination.ToString())
            {
                timeMode = (int)Elimination.TIMEMODE;
                SetModeElimination();
                SetActionNormal();
                SetManagerVoid();
            }
            else if (mode == DeathMatch.DeathMatch.ToString())
            {
                timeMode = (int)DeathMatch.TIMEMODE;
                SetModeDeathMatch();
                SetActionNormal();
                SetManagerVoid();
            }
        }


        //----StartGameMode----

        public void StartThiefGameMode()
        {
            StartCoroutine(StartDecount());
        }

        protected void StartEliminationGameMode()
        {
            StartCoroutine(StartDecount());
        }

        protected void StartDeathMatchMode()
        {
            StartCoroutine(StartDecount());
        }

        public void Pause()
        {
            pause = true;
            lastTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            HUD.Instance.setPause(pause);
        }

        public void UnPause()
        {
            HUD.Instance.setPause(false);
            StartCoroutine(StartDecountUnPause());
        }

        public void IncrementScore(int player, int score)
        {
            switch (player)
            {
                case 1:
                    scoreP1 += score;
                    break;
                case 2:
                    scoreP2 += score;
                    break;
                case 3:
                    scoreP3 += score;
                    break;
                case 4:
                    scoreP4 += score;
                    break;
            }
            Debug.Log(score);
            HUD.Instance.updateScore();
        }

        public IEnumerator StartDecount()
        {
            yield return new WaitForSeconds(.01f);
            SpawnPlayerMode.Instance?.CheckPlayerNumber(NumberOfPlayerMax);
            yield return new WaitForSeconds(.45f);
            HUD.Instance.setStartText("3");
            yield return new WaitForSeconds(.45f);
            HUD.Instance.setStartText("2");
            yield return new WaitForSeconds(.45f);
            HUD.Instance.setStartText("1");
            yield return new WaitForSeconds(.45f);
            HUD.Instance.setStartText("Go !");
            gameStart = true;
            yield return new WaitForSeconds(.45f);
            HUD.Instance.checkNumberOfPlayerHUD(NumberOfPlayerMax);
            HUD.Instance.updateScore();
            HUD.Instance.startText.enabled = false;
            ObjectiveByGameMode();
        }

        private void ObjectiveByGameMode()
        {
            if (mode == BestOfThieves.BestOfThieves.ToString())
            {
                Objective.ChooseObjective();
            }

        }

        public IEnumerator StartDecountUnPause()
        {
            yield return new WaitForSecondsRealtime(.45f);
            HUD.Instance.startText.enabled = true;
            HUD.Instance.setStartText("3");
            yield return new WaitForSecondsRealtime(.45f);
            HUD.Instance.setStartText("2");
            yield return new WaitForSecondsRealtime(.45f);
            HUD.Instance.setStartText("1");
            yield return new WaitForSecondsRealtime(.45f);
            HUD.Instance.setStartText("Go !");
            yield return new WaitForSecondsRealtime(.45f);
            HUD.Instance.startText.enabled = false;
            pause = false;
            Time.timeScale = lastTimeScale;
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void DoActionNormal()
        {
            if (gameStart)
            {
                if (Input.GetKeyDown(controller.start) && !pause)
                {
                    Pause();
                }
                else if (Input.GetKeyDown(controller.start) && pause)
                {
                    UnPause();
                }
            }
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

        }

        //----SetGameMode----

        protected override void SetModeBestOfThief()
        {
            base.SetModeBestOfThief();
            StartThiefGameMode();

        }

        protected override void SetModeElimination()
        {
            base.SetModeElimination();
            StartEliminationGameMode();

        }

        protected override void SetModeDeathMatch()
        {
            base.SetModeDeathMatch();
            StartDeathMatchMode();
        }

        protected override void SetManagerWaitRoom()
        {
            base.SetManagerWaitRoom();
            timeMode = (int)WaitRoom.TIMEMODE;
            gameStart = true;
        }

        protected override void SetManagerWin()
        {
            base.SetManagerWin();
            gameStart = false;

        }

        //----DoActionGameMode----
        protected override void DoModeElimination()
        {
            if (gameStart)
            {
                timeMode -= Time.deltaTime;

                if (timeMode <= 0)
                {
                    CheckWinner();
                    SetManagerWin();
                }
            }
        }

        protected override void DoModeDeathMatch()
        {
            if (gameStart)
            {
                timeMode -= Time.deltaTime;

                if (timeMode <= 0)
                {
                    CheckWinner();
                    SetManagerWin();
                }
            }
        }

        protected override void DoModeBestOfThief()
        {
            if (gameStart)
            {
                timeMode -= Time.deltaTime;

                if (timeMode <= 0)
                {
                    CheckWinner();
                    SetManagerWin();
                }
            }
        }

        protected void SpawnPlayer(int playerNumber)
        {
            if (playerNumber == 1)
            {
                if (!spawnedPlayer1)
                {
                    PlayerSpawner1.SpawnPlayers();
                    NumberOfPlayerMax++;
                    spawnedPlayer1 = true;
                }
            }
            else if (playerNumber == 2)
            {
                if (!spawnedPlayer2)
                {
                    PlayerSpawner2.SpawnPlayers();
                    NumberOfPlayerMax++;
                    spawnedPlayer2 = true;
                    ControllerManager.Instance.RumbleController(playerNumber, 0.1f);

                }
            }
            else if (playerNumber == 3)
            {
                if (!spawnedPlayer3)
                {
                    PlayerSpawner3.SpawnPlayers();
                    NumberOfPlayerMax++;
                    spawnedPlayer3 = true;
                    ControllerManager.Instance.RumbleController(playerNumber, 0.1f);

                }
            }
            else if (playerNumber == 4)
            {
                if (!spawnedPlayer4)
                {
                    PlayerSpawner4.SpawnPlayers();
                    NumberOfPlayerMax++;
                    spawnedPlayer4 = true;
                    ControllerManager.Instance.RumbleController(playerNumber, 0.1f);

                }
            }
        }

        protected override void ManagerWaitRoom()
        {
            timeMode -= Time.deltaTime;

            if (timeMode <= 0)
            {
                gameStart = false;
                TransitionManager.Instance.TransitionToGame(GoToLevel);
                SetManagerVoid();
                CheckMode();
            }
        }

        protected override void ManagerWin()
        {
        }

        protected void GoToLevel()
        {
            Debug.Log("Menu");
            LevelManager.Instance.GoToLevel(level);
        }

        public void CheckWinner()
        {
            List<int> lFinalScore = new List<int>();
            winText = "";
            lFinalScore.Add(scoreP1);
            lFinalScore.Add(scoreP2);
            lFinalScore.Add(scoreP3);
            lFinalScore.Add(scoreP4);
            lFinalScore.Sort();
            if(lFinalScore[lFinalScore.Count - 1] == scoreP1)
            {
                playerNumberWin = 1;
                winText += "Player 1 ";
                ControllerManager.Instance.RumbleController(1, 0.5f);
            }

            if (lFinalScore[lFinalScore.Count - 1] == scoreP2 && NumberOfPlayerMax >= 2)
            {
                playerNumberWin = 2;
                winText += "Player 2 ";
                ControllerManager.Instance.RumbleController(2, 0.5f);
            }

            if (lFinalScore[lFinalScore.Count - 1] == scoreP3 && NumberOfPlayerMax >=3)
            {
                playerNumberWin = 3;
                winText += "Player 3 ";
                ControllerManager.Instance.RumbleController(3, 0.5f);
            }

            if (lFinalScore[lFinalScore.Count - 1] == scoreP4 && NumberOfPlayerMax >= 4)
            {
                playerNumberWin = 4;
                winText += "Player 4 ";
                ControllerManager.Instance.RumbleController(4, 0.5f);
            }
            Time.timeScale = 0.5f;
            WinScreen.Instance.CheckPlayerWin(winText);
            TransitionManager.Instance.TransitionWinner(GoBackToMenu);
        }

        protected void GoBackToMenu()
        {
            Reset();
            LevelManager.Instance.GoToLevel("Menu");
            Time.timeScale = 1;
        }

        public void SetSlowMotion()
        {
            StartCoroutine(SlowMotion(0.25f, 0.35f));
        }

        public void SetSlowMotion(float timeScale)
        {
            StartCoroutine(SlowMotion(timeScale, 0.5f));
        }

        public void SetSlowMotion(float timeScale,float time)
        {
            StartCoroutine(SlowMotion(timeScale, time));
        }

        protected IEnumerator SlowMotion(float timeScale, float time)
        {
            Time.timeScale = timeScale;
            yield return new WaitForSeconds(time);
            Time.timeScale = 1f;
        }
        private void OnDestroy(){
			if (this == instance) instance = null;
		}
	}
}