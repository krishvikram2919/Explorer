using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetUI : MonoBehaviour {
    //Private
    scoLevels.LevelTuple pTuple;
    int LastLevelID;

    //Public
    public int SetID;
    public scoLevels LevelInfo;
    public LevelButtonUI[] btnlevels;

    void Start () {
        Vector3 pos = transform.position;
        pos.x = pos.x + SetID * 75;
        transform.position = pos;
        SetID++;
        LastLevelID = ((SetID * 8 - 1) <= (LevelInfo.TotalLevel - 1)) ? (SetID * 8 - 1) : (LevelInfo.TotalLevel - 1);
        SetID--;
        CreateLevelButtons();
    }
	
	void CreateLevelButtons () {
        int i, j;
        for( i= SetID*8,  j=0; i <= LastLevelID; i++, j++)
        {
            btnlevels[j].gameObject.SetActive(true);
            btnlevels[j].txtScore.text = "";
            btnlevels[j].txtLevel.text = (i+1)+"";
        }
	}

    public void LoadLevel(int index)
    {
        CollectorModeMenu.Instance.InitLevel(SetID * 8 + index);
    }
}
