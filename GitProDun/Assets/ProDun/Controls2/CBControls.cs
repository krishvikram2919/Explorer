using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBControls : MonoBehaviour {
    public enum MoveDir {  Up, Dn, Fwd, Bk,  Lt,Rt, None};
    public Sensor senF, senB, senL, senR, senU, senD, senJ;
    public int Speed = 2;
    [SerializeField] MoveDir curMove;
    [SerializeField] Vector3 mvDir, mWang, fromPos;

    public bool Up, Down, Left, Right, Front, Back, JD;

    public void Start()
    {
        transform.position = new Vector3(9, 1, 0);
        transform.eulerAngles = new Vector3(0, 0, 0);
        curMove = MoveDir.None;
    }


    public void MoveFor(int CtrlDir)
    {
        if (curMove != MoveDir.None)
            return;
        curMove = (MoveDir)(CtrlDir);

        fromPos = transform.position;
        mvDir = transform.position;

        if(curMove== MoveDir.Fwd)
        {
            if (!senF.Hit)
                mvDir = transform.position + transform.forward;
            else
                curMove = MoveDir.None;
        }
        else if (curMove == MoveDir.Bk)
        {
            if (!senB.Hit)
                mvDir = transform.position - transform.forward;
            else
                curMove = MoveDir.None;
        }
        else if (curMove == MoveDir.Rt)
        {
            mWang = transform.eulerAngles;
            mvDir= transform.eulerAngles + (Vector3.up*90);
        }
        else if (curMove == MoveDir.Lt)
        {
            mWang = transform.eulerAngles;
            mvDir = transform.eulerAngles + (Vector3.down * 90);
        }
        else if (curMove == MoveDir.Up)
        {
            if (!senU.Hit)
                mvDir = transform.position + transform.up;
            else
                curMove = MoveDir.None;

        }
        else if (curMove == MoveDir.Dn)
        {
            if (!senD.Hit)
                mvDir = transform.position - transform.up;
            else
                curMove = MoveDir.None;
        }

    }

    public void Update()
    {

        Up = senU.Hit;
        Down = senD.Hit;
        Left = senL.Hit;
        Right = senR.Hit;
        Front = senF.Hit;
        Back = senB.Hit;
        JD = senJ.Hit;

        if (curMove == MoveDir.Fwd || curMove == MoveDir.Bk)
        {
            transform.position = Vector3.MoveTowards(transform.position,  mvDir, Time.deltaTime * Speed *1.5f);
            if (transform.position == mvDir)
            {
                transform.position = mvDir;
                curMove = MoveDir.None;
                StartCoroutine(JumpsFollowAction(false));

            }
        }
        if (curMove == MoveDir.Rt || curMove == MoveDir.Lt)
        {
            mWang = Vector3.MoveTowards(mWang, mvDir, 90f*Time.deltaTime * Speed);
            transform.eulerAngles = mWang;
            if (mWang == mvDir)
            {
                transform.eulerAngles = mvDir;
                curMove = MoveDir.None;
            }
        }
        if (curMove == MoveDir.Up || curMove == MoveDir.Dn)
        {
            transform.position = Vector3.MoveTowards(transform.position, mvDir, 2f*Time.deltaTime * Speed);
            if (transform.position == mvDir)
            {
                transform.position = mvDir;
                if(curMove== MoveDir.Up)
                {
                    curMove = MoveDir.None;
                    StartCoroutine(JumpsFollowAction(true));
                }
                else
                {
                    curMove = MoveDir.None;
                    StartCoroutine(JumpsFollowAction(false));

                }
                //Dn
            }
        }
    }

    IEnumerator JumpsFollowAction(bool isJump)
    {
        yield return null;
        curMove = MoveDir.None;
        fromPos = transform.position;
        mvDir = transform.position;

        if (senJ.Hit && isJump)
        {
            curMove = (MoveDir)(MoveDir.Fwd);

            if (!senF.Hit)
                mvDir = transform.position + transform.forward;
            else
                curMove = MoveDir.None;
        }
        else 
        {
            curMove = (MoveDir)(MoveDir.Dn);
            if (!senD.Hit)
                mvDir = transform.position - transform.up;
            else
                curMove = MoveDir.None;
        }

    }
}
