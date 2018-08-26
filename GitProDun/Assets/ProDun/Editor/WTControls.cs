using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WildTraverse))]
public class WTControls : Editor {
    
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();

        WildTraverse wt = (WildTraverse)target;

        //wt.mAuto = GUILayout.Toggle(wt.mAuto, "mAuto");
        if (GUILayout.Button("Up"))
            wt.Traverse(0);
        if (GUILayout.Button("Down"))
            wt.Traverse(1);

        if (GUILayout.Button("Forward"))
            wt.Traverse(2);
        if (GUILayout.Button("Backward"))
            wt.Traverse(3);

        if (GUILayout.Button("Left"))
            wt.Traverse(4);
        if (GUILayout.Button("Right"))
            wt.Traverse(5);


    }
}
