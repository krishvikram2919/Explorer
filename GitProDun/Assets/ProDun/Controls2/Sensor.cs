using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {
    public bool Hit;
    int count = 0;
	// Use this for initialization
	void Start () {
        Hit = false;
	}

    public void OnEnable()
    {
        Hit = false;
        count = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
            return;
        count++;
        if(count>0)
            Hit = true;
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
            return;
        count--;
        if(count<=0)
            Hit = false;
    }
}
