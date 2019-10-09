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

namespace Com.JellyOwl.ThiefFight.PlayerObject
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public static List<Transform> players = new List<Transform>();
        public static List<Player> playersList = new List<Player>();

        protected Rigidbody rb;
        protected float velocity;
        protected bool handfull = false;
        protected bool isRunning = false;
        protected bool isThrowing = false;
        protected bool isPicking = false;
        protected List<GameObject> CollectableObject = new List<GameObject>();
        protected List<GameObject> PickedObject = new List<GameObject>();
        
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

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            targetGroup = GameObject.FindGameObjectWithTag("TargetGroup").GetComponent<CinemachineTargetGroup>();
            targetGroup.AddMember(transform, 1f, 1f);
            playersList.Add(this);
            players.Add(transform);
            isKilled = false;
            controller = new Controller(PlayerNumber);
            if (GameManager.Instance.mode == DeathMatch.DeathMatch.ToString())
            {
                SetModeDeathMatch();
            }
            else
            {
                SetModeNormal();
            }
        }

        private void SetModeDeathMatch()
        {
            doAction = DoActionDeathmatch;
        }

        public void SetModeNormal()
        {
            doAction = DoActionNormal;
        }

        public void SetModeVoid()
        {
            doAction = DoActionVoid;
        }

        public void SetModeStun()
        {
            Drop();
            ControllerManager.Instance.RumbleController(PlayerNumber, 0.2f);
            particleHit.Play();
            particleStun.Play();
            doAction = DoActionStun;
        }

        // Update is called once per frame
        void Update()
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

        protected void DoActionVoid()
        {

        }

        protected void DoActionNormal()
        {
            DetectVelocity();
            DetectionPickeable();
        }

        protected void DoActionDeathmatch()
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
                SetModeNormal();
            }
        }

        protected void DetectVelocity()
        {
            if (slow)
            {
                velocity = slowWalk;
            }
            else if (slowObjective)
            {
                velocity = walk * ratioWalk;
            }
            else
            {
                velocity = walk;
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
            foreach (GameObject picked in PickedObject)
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
            GameObject lObject;
            for (int i = PickedObject.Count - 1; i >= 0; i--)
            {
                lObject = PickedObject[i];
                PickedObject.Remove(PickedObject[i]);
                lObject.GetComponent<Rigidbody>().isKinematic = false;
                lObject.GetComponent<Collider>().enabled = true;
            }
            handfull = false;
            slowObjective = false;
        }

        private void Pick()
        {
            for (int i = CollectableObject.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    PickedObject.Add(CollectableObject[i]);
                    CollectableObject[i].GetComponent<Rigidbody>().isKinematic = true;
                    CollectableObject[i].GetComponent<Collider>().enabled = false;
                    CollectableObject[i].GetComponent<Outline>().enabled = false;
                    if (CollectableObject[i].GetComponent<Collectible>().isObjective)
                    {
                        slowObjective = true;
                    }
                    CollectableObject[i].GetComponent<Collectible>().LastPlayer = PlayerNumber;
                    CollectableObject.Remove(CollectableObject[i]);
                    handfull = true;
                    isPicking = true;
                    break;
                }

            }
        }

        private void Throw()
        {
            for (int i = PickedObject.Count - 1; i >= 0; i--)
            {
                GameObject lObject = PickedObject[i];
                PickedObject.Remove(PickedObject[i]);
                lObject.transform.position = launch.transform.position;
                lObject.GetComponent<Rigidbody>().isKinematic = false;
                lObject.GetComponent<Collectible>().isThrow = true;
                if (lObject.GetComponent<Rocket>() is null)
                {
                    lObject.GetComponent<Rigidbody>().AddTorque(transform.right * VerticalForce, ForceMode.Impulse);
                    lObject.GetComponent<Rigidbody>().AddForce(transform.forward * HorizontalForce, ForceMode.Impulse);
                    lObject.GetComponent<Rigidbody>().AddForce(transform.up * VerticalForce, ForceMode.Impulse);
                }
                lObject.GetComponent<Collider>().enabled = true;

            }
            handfull = false;
            isThrowing = true;
            slowObjective = false;
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

        

        

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pickeable") && !other.GetComponent<Collectible>().isThrow)
            {
                CollectableObject.Add(other.gameObject);
            }
        }

        protected void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Pickeable") && !other.GetComponent<Collectible>().isThrow)
            {
                CollectableObject = CollectableObject.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToList();
            }
        }

            private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Pickeable"))
            {
                CollectableObject.Remove(other.gameObject);
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
            ControllerManager.Instance.RumbleController(PlayerNumber, 0.3f);
            isKilled = true;
            Drop();
            GameObject lExplosion = Instantiate(Resources.Load<GameObject>("Prefab/Explosion/DieExplosion"));
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

