using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PControl : MonoBehaviour {
    enum MoveType {None, Move,Rotate};
    public Vector3 myloc;
    public Vector3 myAng;
    public Vector3 workingAng;
    Transform t;
    float timer = 0;
    
    MoveType controlType;

    void Start()
    {
        t = gameObject.transform;

        if (gameObject.GetComponent<Cube>() != null)
        {
            myloc = gameObject.GetComponent<Cube>().loc;

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (controlType== MoveType.None)
            return;
        else if(controlType == MoveType.Move)
        {
            timer += Time.deltaTime;
            t.position = Vector3.MoveTowards(t.position, myloc, Time.deltaTime);
            if (t.position == myloc)
                controlType = MoveType.None;
        }
        else if (controlType == MoveType.Rotate)
        {
            timer += Time.deltaTime;
            workingAng = Vector3.MoveTowards(workingAng, myAng, 90*Time.deltaTime);

            if (workingAng == myAng)
                controlType = MoveType.None;

            t.localEulerAngles = workingAng;
        }

    }

    public void Traverse(int mode)
    {
        if (controlType != MoveType.None)
            return;

        int m = Random.Range(2, 999) % 3;
        int x = Random.Range(2, 999) % 2;
        if (mode > -1)
        {
            x = mode % 2;
            m = Mathf.FloorToInt(mode / 2);
        }

        if (m == 0)
        {
            myloc = UpDown((x == 0) ? true : false);
            timer = 0;
            controlType = MoveType.Move;
            if (myloc == t.position)
                controlType = MoveType.None;
        }
        else if (m == 1)
        {
            myloc = FwdBack((x == 0) ? true : false);
            timer = 0;
            controlType = MoveType.Move;
            if (myloc == t.position)
                controlType = MoveType.None;

        }
        else if (m == 2)
        {
            myAng = LtRt((x == 0) ? true : false);

            timer = 0;
            controlType = MoveType.Rotate;

        }


    }

 
    void FakeGravity()
    {
        Traverse(1);
    }

    void Jumpish()
    {
        Traverse(2);
    }

    Vector3 UpDown(bool isUp)
    {
        Vector3 ret = myloc;
        controlType = MoveType.Move;
        if (isUp)
            ret.y++;
        else
            ret.y--;

        if (ret.y < 1 || ret.y > 3)
            return myloc;

        if (CheckBitmap(ret) == 1)
            return myloc;

        myloc = ret;
        if (isUp)
            ret= FwdBack(true);
        return ret;
    }

    Vector3 FwdBack(bool isFwd)
    {
        Vector3 ret = myloc;
        controlType = MoveType.Move;

        if (isFwd)
            ret.z++;
        else
            ret.z--;

        if (ret.z < 0 || ret.z > 9)
            return myloc;

        if (CheckBitmap(ret) == 1)
            return myloc;

        return ret;
    }

    Vector3 LtRt(bool isLt)
    {
        Vector3 ret = myAng;
        workingAng = myAng;

        controlType = MoveType.Rotate;

        if (!isLt)
            ret.y += 90;
        else
        {
            ret.y -= 90;
        }

        //if (ret.x < 0 || ret.x > 9)
        //    return myloc;


        //if (CheckBitmap(ret) == 1)
        //    return myloc;
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
