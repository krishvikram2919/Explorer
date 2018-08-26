using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUI : MonoBehaviour {
    public int pages = 1;
    public int speed = 100;
    public AnimationCurve speedcurve;
    int x = 0;
    Vector3 pos,startpos;
    bool flgMove;
    float t = 0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (flgMove)
        {
            if (transform.position.x != x)
            {
                t += Time.deltaTime;
                pos = transform.localPosition;
                pos.x = x;
                //transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos, Time.deltaTime * speed  );
                transform.localPosition = startpos + (pos - startpos) * speedcurve.Evaluate(t);
            }
            else
                flgMove = false;
        }
	}

    public void Prev()
    {
        x += 725;
        if (x > 0)
            x = 0;

        flgMove = true;
        t = 0;
        startpos = transform.localPosition;

    }

    public void Next()
    {
        x -= 725;
        if (x < pages * -725)
            x = pages * -725;

        flgMove = true;
        t = 0;
        startpos = transform.localPosition;
    }
}
