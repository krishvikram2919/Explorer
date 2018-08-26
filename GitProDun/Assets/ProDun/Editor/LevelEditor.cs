using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProcDung))]

public class LevelEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ProcDung wt = (ProcDung)target;

        EditorGUILayout.LabelField("Level: ", wt.SeedLevel.ToString());
        EditorGUILayout.LabelField("Orbs: ", wt.SeedCollect.ToString());

        //wt.mAuto = GUILayout.Toggle(wt.mAuto, "mAuto");
        if (GUILayout.Button("Randomize Level"))
            wt.CreateLevel();

        if (GUILayout.Button("Randomize Collectible"))
            wt.SetCollectible();


        if (GUILayout.Button("Save Level"))
            wt.SetCollectible();


    }
}
