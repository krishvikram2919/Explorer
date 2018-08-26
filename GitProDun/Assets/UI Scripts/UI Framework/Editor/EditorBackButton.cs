using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BackButton))]
 public class edBackButton : Editor
 {
    BackButton mInstance;
     void OnEnable()
     {
		mInstance = (BackButton)target;
     }
     public override void OnInspectorGUI()
     {
        base.OnInspectorGUI();
		switch(mInstance.BackType)
         {
			case BackButton.type.Scene:
			mInstance.sceneName = EditorGUILayout.TextField("Scene Name", mInstance.sceneName );
                    break;
			case BackButton.type.BackPanel:
					mInstance.Panel =(GameObject)EditorGUILayout.ObjectField("Back Panel", mInstance.Panel, typeof(GameObject), true);
					break;
		    case BackButton.type.Popup:
                //mInstance.prevBackButton = (BackButton)EditorGUILayout.ObjectField("prevBackButton", mInstance.prevBackButton, typeof(BackButton), true);
                mInstance.Popup =(GameObject)EditorGUILayout.ObjectField("Popup", mInstance.Popup, typeof(GameObject), true);
    			break;

            case BackButton.type.None:
                break;
         }
     }
 }
