using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildTraverse : MonoBehaviour {
    public  Vector3 myloc;
    Transform t;
    float timer = 0;
    public bool mAuto;

    void Start () {
        t = gameObject.transform;

        if (gameObject.GetComponent<Cube>() != null)
        {
            myloc = gameObject.GetComponent<Cube>().loc;
            mAuto = true;
        }

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        //if (timer > 0.01f)
        {
            timer = 0;
            if(mAuto)
                Traverse(-1);
        }

	}

    public void Traverse(int mode)
    {
        int m = Random.Range(2, 999) % 3;
        int x = Random.Range(2, 999) % 2;
        if (mode > -1)
        {
            x = mode % 2;
            m = Mathf.FloorToInt(mode / 2);
        }

        if (m == 0)
            myloc = UpDown((x == 0) ? true : false);
        else if(m==1)
            myloc  = FwdBack((x == 0) ? true : false);
        else if (m == 2)
            myloc = LtRt((x == 0) ? true : false);

        t.position = myloc;
    }





    Vector3 UpDown(bool isUp)
    {
        Vector3 ret = myloc;

        if (isUp)
            ret.y++;
        else
            ret.y--;

        if (ret.y < 1 || ret.y>3)
            return myloc;

        if (CheckBitmap(ret) == 1)
            return myloc;

        return ret;
    }

    Vector3 FwdBack(bool isFwd)
    {
        Vector3 ret = myloc;

        if (isFwd)
            ret.x++;
        else
            ret.x--;

        if (ret.x < 0 || ret.x > 9)
            return myloc;

        if (CheckBitmap(ret) == 1 )
            return myloc;
        return ret;
    }

    Vector3 LtRt(bool isLt)
    {
        Vector3 ret = myloc;

        if (isLt)
            ret.z++;
        else
            ret.z--;

        if (ret.z < 0 || ret.z > 9)
            return myloc;


        if (CheckBitmap(ret) == 1)
            return myloc;
        return ret;
    }

    int CheckBitmap(Vector3 pLoc)
    {
        int x, y, z;
        x = Mathf.RoundToInt(pLoc.x);
        y = Mathf.RoundToInt(pLoc.y);
        z = Mathf.RoundToInt(pLoc.z);
        return ProcDung.bitmap[x, y, z];
    }
}
