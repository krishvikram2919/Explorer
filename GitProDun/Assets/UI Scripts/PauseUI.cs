using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : UIPanelBase {
    public GameObject Ingame;
    public GameObject Menu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	public void ExitGame () {
        Ingame.GetComponent<IngameUI>().Close();
        Menu.GetComponent<EntryAnim>().ActivateUI();
        GetComponent<BackButton>().OnBackButton();
	}
}
