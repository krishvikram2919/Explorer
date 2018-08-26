using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : UIPanelBase {
    public EntryAnim NextScreen;
    //Set aEntry to Menu
	void OnEnable () {
        NextScreen.Invoke("ActivateUI", 1f);
        aExit.Invoke("DeactivateUI", 1f);
    }
	
}
