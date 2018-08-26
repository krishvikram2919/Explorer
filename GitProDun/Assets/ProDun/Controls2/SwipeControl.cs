using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour {
    public CBControls control;

    Vector3 mStart, mEnd;
    Vector3 mDirection;
    float duration=-1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            mStart = Input.mousePosition;
            if(duration<0)
                duration = Time.time;
            else
            {
                duration = Time.time - duration;
                if (duration < 0.5f)
                {
                    control.MoveFor((int)CBControls.MoveDir.Up);
                }
                duration = -1f;

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            mEnd = Input.mousePosition;
            if (Vector3.Distance(mEnd, mStart) > 100f)
            {
                duration = -1f;

                mDirection = mEnd - mStart;
                if (Mathf.Abs(mDirection.x) > Mathf.Abs(mDirection.y))
                {
                    if (mDirection.x < 0)
                        control.MoveFor((int)CBControls.MoveDir.Lt);
                    else
                        control.MoveFor((int)CBControls.MoveDir.Rt);
                }
                else
                {
                    if (mDirection.y < 0)
                        control.MoveFor((int)CBControls.MoveDir.Bk);
                    else
                        control.MoveFor((int)CBControls.MoveDir.Fwd);
                }
            }
        }      
	}


}
