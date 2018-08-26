using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FpsTest : MonoBehaviour {
	public static FpsTest instance;

	float fpsMeasurePeriod = 0.5f;
	int fpsAccumulator = 0;
	float fpsNextPeriod = 0;
	int currentFps;
	string display = "{0} FPS";
	[SerializeField] Text guiDisplay;

	void Awake(){
		if (instance == null)		
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    

		DontDestroyOnLoad(this.gameObject);
	}

	void Start () {
		Application.targetFrameRate = 60;
		fpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
	}
	

	void Update () {
		fpsAccumulator++;
		if (Time.realtimeSinceStartup > fpsNextPeriod)
		{
			currentFps = (int)(fpsAccumulator / fpsMeasurePeriod);
			fpsAccumulator = 0;
			fpsNextPeriod += fpsMeasurePeriod;
            guiDisplay.text = string.Format(display, currentFps);
        }
	}
}
