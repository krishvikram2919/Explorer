using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameUI : UIPanelBase,IBackAction {
    public static IngameUI Instance;
    public EntryAnim WinPopup;

    public GameObject GameSetup;
	// Use this for initialization
	void Start () {
        Instance = this;

    }

    public void LevelCompleted()
    {
        WinPopup.ActivateUI();
    }

    void IBackAction.BackAction()
    {
        //CollectorModeMenu.Instance.Setup.SetActive(false);
    }

    public void Close()
    {
        aExit.DeactivateUI();
        GameSetup.SetActive(false);

    }
}
