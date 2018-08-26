using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CBControls))]
public class CBControlEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CBControls wt = (CBControls)target;

        //wt.mAuto = GUILayout.Toggle(wt.mAuto, "mAuto");
        if (GUILayout.Button("Up"))
            wt.MoveFor(0);
        //if (GUILayout.Button("Down"))
            //wt.MoveFor(1);

        if (GUILayout.Button("Forward"))
            wt.MoveFor(2);
        if (GUILayout.Button("Backward"))
            wt.MoveFor(3);

        if (GUILayout.Button("Left"))
            wt.MoveFor(4);
        if (GUILayout.Button("Right"))
            wt.MoveFor(5);


    }

}
