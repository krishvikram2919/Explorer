using UnityEngine;
using System.Collections;

public enum PlayerType{X,Y,Z};

public enum Zbehaviour{Za,Zb,Zc};
public class MyTest : MonoBehaviour {
	public PlayerType type;
	public string objName;
	public GameObject addon;
	public Zbehaviour zType;
}
