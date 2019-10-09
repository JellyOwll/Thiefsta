using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.JellyOwl.ThiefFight.AI
{
    public class Ai : MonoBehaviour
    {
        protected Action doAction;
        protected Action doActionFixed;
        protected Material WireClose;
        protected Material WireOpen;

        [Range(0,360)]
        public float viewAngle;
        public float viewRadius;

        protected Vector3 viewPoint;
        

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        protected bool isVisible;

        public List<Transform> visibleTargets = new List<Transform>();
        // Start is called before the first frame update
        virtual protected void Start()
        {
            setModeVoid();
            WireClose = Resources.Load("Materials/RedWire") as Material;
            WireOpen = Resources.Load("Materials/GreenWire") as Material;
            //StartCoroutine(FindTargetsWithDelay(.1f));
        }

        IEnumerator FindTargetsWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }

        // Update is called once per frame
        void Update()
        {
            doAction();
        }
        private void FixedUpdate()
        {
            doActionFixed();
        }

        public void setModeVoid()
        {
            doAction = doActionVoid;
            doActionFixed = doActionVoidFixed;
        }

        private void doActionVoidFixed()
        {

        }

        private void doActionVoid()
        {
        }

        public void setModePatrol()
        {
            doActionFixed = doActionPatrolFixed;
            doAction = doActionPatrol;
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y +90;
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        protected void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = targetsInViewRadius.Length - 1; i >= 0; i--)
            {
                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if(Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if(!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                        Debug.DrawLine(transform.position, target.transform.position);
                    }
                }
            }
        }
        public void setModeTurn()
        {
            doActionFixed = doActionTurnFixed;
            doAction = doActionTurn;

        }

        virtual public void setModeChase()
        {
            doActionFixed = doActionChaseFixed;
            doAction = doActionChase;
        }

        virtual protected void doActionChaseFixed()
        {
            
        }

        virtual protected void doActionChase()
        {

        }

        virtual protected void doActionTurnFixed()
        {

        }

        virtual protected void doActionTurn()
        {

        }

        virtual protected void doActionPatrolFixed()
        {

        }

        virtual protected void doActionPatrol()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Vector3 dirToTarget = (other.transform.position - transform.position).normalized;
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dirToTarget,out hit,targetMask))
                {
                    if(hit.collider.CompareTag("Player"))
                    {
                        Debug.DrawLine(transform.position, other.transform.position, Color.red);
                        Debug.Log("Visible");
                        viewPoint = hit.point;
                        isVisible = true;
                        
                    } else
                    {
                        Debug.DrawLine(transform.position, hit.point, Color.white);
                        Debug.Log("NonVisible");
                        isVisible = false;
                    }
                }
                else
                {
                    
                }

            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isVisible = false;
            }
        }


    }
}