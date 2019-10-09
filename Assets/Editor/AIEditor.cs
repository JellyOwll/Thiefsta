///-----------------------------------------------------------------
/// Author : Teo Diaz
/// Date : 17/09/2019 14:49
///-----------------------------------------------------------------

using Com.JellyOwl.ThiefFight.AI;
using UnityEditor;
using UnityEngine;

namespace Com.JellyOwl.AI{

    [CustomEditor(typeof(Ai))]
	public class AIEditor : Editor {
        private void OnSceneGUI()
        {
            Ai fow = (Ai)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fow.transform.position, fow.transform.right, fow.transform.forward, 360, fow.viewRadius);
            Handles.color = Color.red;
            Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
            Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
            Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);


        }
    }
}