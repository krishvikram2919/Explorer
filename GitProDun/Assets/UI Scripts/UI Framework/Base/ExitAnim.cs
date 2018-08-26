using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitAnim : UITransformationWithCurves {

    // Use this for initialization
    void Awake()
    {
        base.iAwake();
        PlayOnStart = false;
        ReplayOnEnable = false;
        DisableAfterAnim = true;
    }

    void Start()
    {
        base.iStart();
    }

    private void OnEnable()
    {
        base.iOnEnable();
    }

	/// <summary>
	/// Deactivate this instance.
	/// </summary>
	public void DeactivateUI(){
		Animate();
	}
}
