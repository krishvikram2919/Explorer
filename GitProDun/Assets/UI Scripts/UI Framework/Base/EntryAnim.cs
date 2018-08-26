using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryAnim : UITransformationWithCurves
{

    // Use this for initialization
    void Awake()
    {
        base.iAwake();
        PlayOnStart = true;
        ReplayOnEnable = true;
        DisableAfterAnim = false;
    }

    void Start()
    {
        base.iStart();
        if (PlayOnStart)
        {
            Animate();
        }
    }

    private void OnEnable()
    {
        base.iOnEnable();
    }


	/// <summary>
	/// Activate this instance.
	/// </summary>
	public void ActivateUI(){
		gameObject.SetActive(true);
		Animate();
	}

}
