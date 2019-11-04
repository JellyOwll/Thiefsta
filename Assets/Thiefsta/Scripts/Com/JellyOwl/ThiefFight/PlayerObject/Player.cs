///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 15/09/2019 02:38
///-----------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;
using System;
using Com.JellyOwl.ThiefFight.Managers;
using Com.JellyOwl.ThiefFight.Collectibles;
using UnityEngine.InputSystem;
using System.Collections;
using Com.JellyOwl.ThiefFight.StateMachine;
using UnityEngine.Events;

namespace Com.JellyOwl.ThiefFight.PlayerObject {
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]

    public class Player : StateMachineGameMode
    {
        public delegate void PlayerEventHandler(Player sender);

        private const string explosionPath = "Prefab/Explosion/DieExplosion";
        public static List<Transform> players = new List<Transform>();
        public static List<Player> playersList = new List<Player>();

        protected Rigidbody rb;
        protected float velocity;
        protected bool handfull = false;
        protected bool isRunning = false;
        protected bool isThrowing = false;
        protected bool isPicking = false;
        public List<Collectible> CollectableObject = new List<Collectible>();
        public List<Collectible> PickedObject = new List<Collectible>();
        
        protected Action doAction;
        protected float timerStunMax = 3;
        protected float timerStun = 3;

        [Header("Local Multiplayer")]
        public int PlayerNumber;
        protected CinemachineTargetGroup targetGroup;
        protected Controller controller;

        protected Vector3 vectorVelocity;

        [Space]
        [Header("Parameters")]

        [Range(200f,450f)]
        public float walk = 450f;

        [Range(0.1f, 1f)]
        public float ratioWalk = 0.7f;

        [Range(100f, 175f)]
        public float slowWalk = 150f;

        [Range(400f, 500f)]
        public float run = 450f;

        [HideInInspector]
        public bool slow = false;

        [HideInInspector]
        public bool slowObjective = false;

        [Space]
        [Header("LaunchParameter")]
        public float HorizontalForce = 25f;
        public float VerticalForce = 35f;
        [Space]
        [Header("Useful")]
        public GameObject launch;

        [Space]
        [Header("Effect")]
        [SerializeField]
        protected ParticleSystem particleRun;
        [SerializeField]
        protected ParticleSystem particleHit;
        [SerializeField]
        protected ParticleSystem particleStun;
        [NonSerialized]
        public bool isKilled;
        [SerializeField]
        public ObjectiveArrow objectiveArrow;

        [Header("Custom")]
        [SerializeField]
        protected Transform customAnchor;
        protected GameObject customOutfit;

        public static event PlayerEventHandler OnThrow;
        public static event PlayerEventHandler OnDrop;
        public static event PlayerEventHandler OnPick;
        public static event PlayerEventHandler OnStun;
        public static event PlayerEventHandler OnKilled;

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            CheckOutfit();
            rb = GetComponent<Rigidbody>();
            targetGroup = GameObject.FindGameObjectWithTag("TargetGroup").GetComponent<CinemachineTargetGroup>();
            targetGroup.AddMember(transform, 1f, 1f);
            playersList.Add(this);
            players.Add(transform);
            isKilled = false;
            controller = new Controller(PlayerNumber);
            SetActionNormal();
            objectiveArrow.CheckArrow();
            if (GameManager.Instance.mode == DeathMatch.DeathMatch.ToString())
            {
                SetModeDeathMatch();
            }
            else
            {
                SetModeBestOfThief();
            }
        }

        private void CheckOutfit()
        {
            if(PlayerNumber == 1)
            {
                customOutfit = Resources.Load<GameObject>("Prefab/Outfit/Outfit" + GameManager.Instance.CustomIndexPlayer1);
            }else if (PlayerNumber == 2)
            {
                customOutfit = Resources.Load<GameObject>("Prefab/Outfit/Outfit" + GameManager.Instance.CustomIndexPlayer2);
            }
            else if(PlayerNumber == 3)
            {
                customOutfit = Resources.Load<GameObject>("Prefab/Outfit/Outfit" + GameManager.Instance.CustomIndexPlayer3);
            }
            else if(PlayerNumber == 4)
            {
                customOutfit = Resources.Load<GameObject>("Prefab/Outfit/Outfit" + GameManager.Instance.CustomIndexPlayer4);
            }
            if (customOutfit)
            {
                customOutfit = Instantiate(customOutfit, customAnchor);
                customAnchor.transform.localPosition = Vector3.zero;
            }
        }

        protected override void DoActionNormal()
        {
            if (GameManager.Instance.gameStart)
            {
                if (!(doAction is null))
                {
                    doAction();
                }
                if (rb.velocity == Vector3.zero)
                {
                    if (particleRun.isEmitting)
                    {
                        particleRun.Stop();
                    }
                }
                else
                {
                    if (!particleRun.isEmitting)
                    {
                        particleRun.Play();
                    }
                    else
                    {
                        ParticleSystem.MainModule main = particleRun.main;
                        float mag = Mathf.Clamp01(new Vector2(Input.GetAxis("HorizontalP" + PlayerNumber.ToString()), Input.GetAxis("VerticalP" + PlayerNumber.ToString())).magnitude);
                        main.startSize = new ParticleSystem.MinMaxCurve(velocity * .0001f, velocity * .003f * mag);
                    }
                }
            }
            else
            {
                velocity = 0;
            }
        }

        public void SetModeStun()
        {
            OnStun.Invoke(this);
            Drop();
            ControllerManager.Instance.RumbleController(PlayerNumber, 0.2f);
            particleHit.Play();
            particleStun.Play();
            doAction = DoActionStun;
        }

        protected override void DoModeBestOfThief()
        {
            DetectVelocity();
            DetectionPickeable();
        }

        protected override void DoModeElimination()
        {

        }

        protected override void DoModeDeathMatch()
        {
            DetectVelocity();
            DetectionPickeable();
        }

        private void DoActionStun()
        {
            timerStun -= Time.deltaTime;
            velocity = slowWalk;

            if (timerStun <= 0)
            {
                particleStun.Stop();
                timerStun = timerStunMax;
                SetActionNormal();
            }
        }

        protected void DetectVelocity()
        {
            if (slow)
            {
                velocity = slowWalk;
            }
            else
            {
                velocity = slowObjective ? walk * ratioWalk : walk;
            }
        }

        private void DetectionPickeable()
        {
            OutlineDetector();
            if (!(controller is null))
            {
                if (Input.GetKeyDown(controller.b) && handfull)
                {
                    Drop();

                }
                if (Input.GetKeyDown(controller.a) && !handfull && !isThrowing)
                {
                    Pick();
                }

                if (Input.GetKeyDown(controller.a) && handfull && !isPicking)
                {
                    Throw();
                }
            }
            foreach (Collectible picked in PickedObject)
            {
                picked.transform.position = launch.transform.position;
                picked.transform.rotation = transform.rotation;

            }
            isPicking = false;
            isThrowing = false;
        }

        private void OutlineDetector()
        {
            for (int i = CollectableObject.Count - 1; i >= 0; i--)
            {
                if (!CollectableObject[i].GetComponent<Collider>().enabled)
                {
                    CollectableObject.RemoveAt(i);
                    continue;
                }
                CollectableObject[i].GetComponent<Outline>().enabled = i == 0;
            }
        }

        public void Drop()
        {
            OnDrop?.Invoke(this);
            handfull = false;
            slowObjective = false;
            objectiveArrow.Followtruck = false;

        }

        private void Pick()
        {
            OnPick?.Invoke(this);
            handfull = true;
            isPicking = true;
        }

        private void Throw()
        {
            OnThrow?.Invoke(this);
            handfull = false;
            isThrowing = true;
            slowObjective = false;
            objectiveArrow.Followtruck = false;

        }

        private void FixedUpdate()
        {
            if(doAction != DoActionVoid)
            {
                vectorVelocity = new Vector3(Input.GetAxis("HorizontalP" + PlayerNumber.ToString()) * velocity * Time.fixedDeltaTime, rb.velocity.y, Input.GetAxis("VerticalP" + PlayerNumber.ToString()) * velocity * Time.fixedDeltaTime);
                if (Input.GetAxis("HorizontalP" + PlayerNumber.ToString()) != 0f || Input.GetAxis("VerticalP" + PlayerNumber.ToString()) != 0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("HorizontalP" + PlayerNumber.ToString()), 0f, Input.GetAxis("VerticalP" + PlayerNumber.ToString())));
                }
                rb.velocity = vectorVelocity;
            }
        }

        

        

        /*private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Collectible>() && !other.GetComponent<Collectible>().isThrow)
            {
                CollectableObject.Add(other.GetComponent<Collectible>());
            }
        }*/

        protected void OnTriggerStay(Collider other)
        {

            if (other.GetComponent<Collectible>() && !other.GetComponent<Collectible>().isThrow)
            {
                if(!CollectableObject.Contains(other.GetComponent<Collectible>()))
                {
                    CollectableObject.Add(other.GetComponent<Collectible>());
                }
                CollectableObject = CollectableObject.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToList();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Collectible>())
            {
                CollectableObject.Remove(other.GetComponent<Collectible>());
                other.GetComponent<Outline>().enabled = false;
            }
        }

        private void OnDestroy()
        {
            players.Remove(transform);
            targetGroup.RemoveMember(transform);
            playersList.Remove(this);
            for (int i = CollectableObject.Count - 1; i >= 0; i--)
            {
                CollectableObject.RemoveAt(i);
            }
        }

        public void Killed()
        {
            OnKilled?.Invoke(this);
            ControllerManager.Instance.RumbleController(PlayerNumber, 0.3f);
            isKilled = true;
            Drop();
            GameObject lExplosion = Instantiate(Resources.Load<GameObject>(explosionPath));
            lExplosion.transform.position = transform.position;
            GameObject[] lSpawners = GameObject.FindGameObjectsWithTag("Respawn");
            foreach (GameObject spawner in lSpawners)
            {
                if (spawner.GetComponent<SpawnPlayer>()?.playerSpawnerNumber == PlayerNumber)
                {
                    spawner.GetComponent<SpawnPlayer>().SpawnAfterSeconds(2);
                }
            }
            Destroy(gameObject);
        }
    }
}

