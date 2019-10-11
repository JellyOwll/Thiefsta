///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 10:29
///-----------------------------------------------------------------

using Cinemachine;
using Com.JellyOwl.ThiefFight.Menus;
using Com.JellyOwl.ThiefFight.ObjectiveObject;
using Com.JellyOwl.ThiefFight.PlayerObject;
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
        TIMEMODE = 5
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


	public class GameManager : MonoBehaviour {
		private static GameManager instance;
		public static GameManager Instance { get { return instance; } }

        [Header("All Mode")]
        protected Controller controller;
        public float timeMode;
        public int NumberOfPlayerMax;

        [Space]
        [Header("Game")]
        protected Objective currentObjective;
        public String mode;
        public String winText;
        public CinemachineVirtualCamera virtualCameraPlayer;
        protected float lastTimeScale;
        protected Action DoAction;
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


        private void Awake(){
			if (instance){
				Destroy(gameObject);
				return;
			}
			
			instance = this;
		}

        public void Start()
        {
            gameStart = true;
            controller = new Controller();
            if (LevelManager.Instance.CheckActiveLevel("WaitRoom"))
            {
                SetWaitRoom();
            } else if (LevelManager.Instance.CheckActiveLevel("Menu"))
            {
                DoAction = null;
                Reset();
            } else if (LevelManager.Instance.CheckActiveLevel("HUB"))
            {
                SetHUB();
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
        }

        private void CheckMode()
        {
            scoreP1 = scoreP2 = scoreP3 = scoreP4 = 0;
            virtualCameraPlayer = GameObject.FindGameObjectWithTag("CMCamPlayer").GetComponent<CinemachineVirtualCamera>();

            if (mode == BestOfThieves.BestOfThieves.ToString())
            {
                timeMode = (int)BestOfThieves.TIMEMODE;
                SetThiefGameMode();
            } else if (mode == Elimination.Elimination.ToString())
            {
                timeMode = (int)Elimination.TIMEMODE;
                SetEliminationGameMode();
            } else if (mode == DeathMatch.DeathMatch.ToString())
            {
                timeMode = (int)DeathMatch.TIMEMODE;
                setDeathMatch();
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
            for (int i = Player.playersList.Count - 1; i >= 0; i--)
            {
                Player.playersList[i].SetModeNormal();
            }
            ObjectiveByGameMode();
        }

        private void ObjectiveByGameMode()
        {
            if(mode == BestOfThieves.BestOfThieves.ToString())
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

        private void Update () {
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
            if(!(DoAction is null) && !pause)
            {
                DoAction();
            }

            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        //----SetGameMode----

        protected void SetThiefGameMode()
        {
            DoAction = DoActionThiefGameMode;
            StartThiefGameMode();
        }

        protected void SetEliminationGameMode()
        {
            DoAction = DoActionElimination;
            StartEliminationGameMode();
        }

        protected void setDeathMatch()
        {
            DoAction = DoActionDeathMatch;
            StartDeathMatchMode();
        }
        

        protected void SetHUB()
        {
            Controller.checkController();
            DoAction = DoActionHUB;
        }

        protected void SetWaitRoom()
        {
            for (int i = Player.playersList.Count - 1; i >= 0; i--)
            {
                Player.playersList[i].SetModeNormal();
            }
            timeMode = (int)WaitRoom.TIMEMODE;
            gameStart = true;
            DoAction = DoActionWaitRoom;
        }

        protected void setModeWinner()
        {
            DoAction = DoActionWinner;
            gameStart = false;
        }

        //----DoActionGameMode----
        protected void DoActionElimination()
        {
            if (gameStart)
            {
                timeMode -= Time.deltaTime;

                if (timeMode <= 0)
                {
                    CheckWinner();
                    setModeWinner();
                }
            }
        }

        protected void DoActionDeathMatch()
        {
            if (gameStart)
            {
                timeMode -= Time.deltaTime;

                if (timeMode <= 0)
                {
                    CheckWinner();
                    setModeWinner();
                }
            }
        }

        protected void DoActionThiefGameMode()
        {
            if (gameStart)
            {
                timeMode -= Time.deltaTime;

                if(timeMode <= 0)
                {
                    CheckWinner();
                    setModeWinner();
                }

                /*if(Mathf.Round(timeMode) == 45 && !importantObjectiveSpawned)
                {
                    importantObjectiveSpawned = true;
                    GameObject[] spawnerObjectives = GameObject.FindGameObjectsWithTag("ImportantObjectiveSpawner");
                    foreach (GameObject item in spawnerObjectives)
                    {
                        Objective.currentObjective.isObjective = false;
                        Objective.currentObjective.CheckIsObjective(false);
                        item.GetComponent<SpawnerImportantObjective>().SpawnImportantObjective();
                    }
                    Debug.Log("SpawnImportantObjective");
                }*/
                /*Debug.Log(Camera.main.transform.position.y);
                if (Camera.main.transform.position.y >= 227.7641f)
                {
                    HUD.Instance.CamSpawn();
                }
                else
                {
                    HUD.Instance.CamRemove();

                }*/
            }
        }

        
        protected void DoActionHUB()
        {
            for (int i = Controller.joystickList.Length - 1; i >= 0; i--)
            {
                Controller lController = new Controller(i +1);
                if (Input.GetKeyDown(lController.start)){
                    Debug.Log("Controller " + i + ": Start");
                    SpawnPlayer(i+1);
                }
            }
        }

        protected void SpawnPlayer(int playerNumber)
        {
            if(playerNumber == 1)
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


        protected void DoActionWinner()
        {

        }

        protected void DoActionWaitRoom()
        {
            for (int i = Player.playersList.Count - 1; i >= 0; i--)
            {
                Player.playersList[i].SetModeNormal();
            }
            timeMode -= Time.deltaTime;

            if(timeMode <= 0)
            {
                gameStart = false;
                LevelManager.Instance.GoToLevel("MuseumNormal");
            }
        }

        public void CheckWinner()
        {
            List<int> lFinalScore = new List<int>();
            winText = null;
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
            Time.timeScale =0.5f;
            WinScreen.Instance.CheckPlayerWin(winText);
            TransitionManager.Instance.TransitionWinner();
            //StartCoroutine(GoBackToMenu());
        }

        protected IEnumerator GoBackToMenu()
        {
            yield return new WaitForSecondsRealtime(3f);
            LevelManager.Instance.GoToLevel("Menu");
            Time.timeScale = 1;

        }

        public void setSlowMotion()
        {
            StartCoroutine(SlowMotion(0.25f, 0.35f));
        }

        public void setSlowMotion(float timeScale)
        {
            StartCoroutine(SlowMotion(timeScale, 0.5f));
        }

        public void setSlowMotion(float timeScale,float time)
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