using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanelManager : MonoBehaviour {
    static PanelManager instance;

    public static PanelManager Instance
    {
        get
        {
            return instance;
        }
    }
    public EntryAnim firstPanel;
    public GameObject[] panels;
	
	void Awake () {
        instance = this;
	}

    //Temp: UI Triggered Start
    public void Start()
    {
        ResetScreens();
    }


    //Called by Game Manager after Init and Before Menu
    public void ResetScreens()
    {
        for (int i = 0; i < panels.Length; i++)
            panels[i].SetActive(false);
        firstPanel.gameObject.SetActive(true);
        firstPanel.ActivateUI();
    }

}
