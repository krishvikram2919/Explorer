using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorModeMenu : UIPanelBase {
    public static CollectorModeMenu Instance;
    public EntryAnim NextPanel;
    public ProcDung Generator;
    public GameObject Setup;

    [Header("Set Creation")]
    [SerializeField] Transform setHolder;
    public GameObject pfSet;
    public scoLevels LevelInfo;
    scoLevels.LevelTuple pTuple;

    
    // Use this for initialization
    void Start () {
        Instance = this;
        CreateSets();
    }

    void CreateSets()
    {
        int setrequired = Mathf.RoundToInt( Mathf.Ceil(LevelInfo.TotalLevel / 8f) );
        pfSet.SetActive(true);

        for (int i =0; i < setrequired; i++)
        {
            GameObject goSet = Instantiate(pfSet) as GameObject;
            goSet.transform.parent = setHolder;

            goSet.GetComponent<LevelSetUI>().SetID = i;
            goSet.SetActive(true);
        }
        pfSet.SetActive(false);
    }
    public void InitLevel(int level)
    {
        Setup.SetActive(true);
        pTuple = LevelInfo.levelList[level];
        Generator.SeedLevel = pTuple.Maze;
        Generator.SeedCollect = pTuple.Maze;
        Generator.InitialiseLevel();
        NextPanel.ActivateUI();
        aExit.DeactivateUI();

    }


}
