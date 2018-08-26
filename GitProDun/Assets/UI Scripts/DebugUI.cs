using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour {
	public static DebugUI instance;
	System.Text.StringBuilder str = new System.Text.StringBuilder();
	[SerializeField] Text guiDisplay;

	void Awake(){
		if (instance == null)		
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    

		DontDestroyOnLoad(this.gameObject);
	}
	public void Log(string pStr){
		str.Remove(0,str.Length);
		str.Insert(0,pStr);
		guiDisplay.text = str.ToString();

	}
}
