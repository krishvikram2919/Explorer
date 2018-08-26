using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnim : MonoBehaviour {
    public AnimCurve3 Forward;
    public AnimCurve3 Left;
    public AnimCurve3 Up;
    

    AnimCurve3 workingCurve;
    int Workingmode = -1;
    float dir = 1;
    public void Animate(int mode)
    {
        Workingmode = mode;
        switch (mode)
        {
            case 0:
                break;
            case 1:
                break;
            case 2://FW
                workingCurve = Forward;
                dir = 1;
                break;
            case 3://BK
                workingCurve = Forward;
                dir = -1;
                break;
            case 4:
                break;
            case 5:
                break;
        }

    }
	
}
