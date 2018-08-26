using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwap : MonoBehaviour {
    public Camera player;
    public Camera map;

    bool flgMapMini = true;
	// Use this for initialization
	void Start () {
		
	}
	
    public void Swap()
    {
        flgMapMini = !flgMapMini;

        if (flgMapMini)
        {
            player.rect = new Rect(0, 0, 1, 1);
            map.rect = new Rect(0.8f, 0.8f, 0.2f, 0.2f);
            map.depth = 1;
            player.depth = 0;
        }
        else
        {
            map.rect = new Rect(0, 0, 1, 1);
            player.rect = new Rect(0.8f, 0.8f, 0.2f, 0.2f);
            map.depth = 0;
            player.depth = 1;
        }
    }
}
