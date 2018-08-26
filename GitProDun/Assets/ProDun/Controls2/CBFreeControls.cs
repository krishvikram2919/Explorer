using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CBFreeControls : MonoBehaviour {
    public enum MoveDir {  Up, Dn, Fwd, Bk,  Lt,Rt, SL,SR, None};
    public Sensor senF, senB, senL, senR, senU, senD, senJ;
    public int Speed = 2;

#region Jump CBC
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


    IEnumerator JumpsFollowAction(bool isJump)
    {
        yield return null;
        curMove = MoveDir.None;
        fromPos = transform.position;
        mvDir = transform.position;

        if (!senF.Hit && senJ.Hit && isJump)
        {
            curMove = (MoveDir)(MoveDir.Fwd);
                mvDir = transform.position + transform.forward * 0.5f;
                FBMove = MoveDir.None;
                LRRotate = MoveDir.None;
        }
        else 
        {
            curMove = (MoveDir)(MoveDir.Dn);
            if (!senD.Hit)
            {
                mvDir = transform.position - transform.up;
                FBMove = MoveDir.None;
                LRRotate = MoveDir.None;
            }
            else
                curMove = MoveDir.None;
        }

    }
#endregion

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
            transform.position = Vector3.MoveTowards(transform.position, mvDir, Time.deltaTime * Speed * 1.5f);
            if (transform.position == mvDir)
            {
                transform.position = mvDir;
                curMove = MoveDir.None;
                StartCoroutine(JumpsFollowAction(false));

            }
        }
        
        else if (curMove == MoveDir.Rt || curMove == MoveDir.Lt)
        {
            mWang = Vector3.MoveTowards(mWang, mvDir, 90f * Time.deltaTime * Speed);
            transform.eulerAngles = mWang;
            if (mWang == mvDir)
            {
                transform.eulerAngles = mvDir;
                curMove = MoveDir.None;
            }
        }
        
        else if (curMove == MoveDir.Up || curMove == MoveDir.Dn)
        {
            transform.position = Vector3.MoveTowards(transform.position, mvDir, 2f * Time.deltaTime * Speed);
            if (transform.position == mvDir)
            {
                transform.position = mvDir;
                if (curMove == MoveDir.Up)
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
        //FREE COLLIDER CONTROL CODE SECTION
        if (FBMove != MoveDir.None)
        {
            if(FBMove == MoveDir.Fwd)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position+ transform.forward, Time.deltaTime * Speed );
                if (senF.Hit)
                {
                    FBMove = MoveDir.None;
                }

                if (curMove == MoveDir.None)
                {
                    StartCoroutine(JumpsFollowAction(false));

                }
            }
            else if (FBMove == MoveDir.Bk)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.forward, Time.deltaTime * Speed);
                if (senB.Hit)
                {
                    FBMove = MoveDir.None;
                }

                if (curMove == MoveDir.None)
                {
                    StartCoroutine(JumpsFollowAction(false));

                }
            }
            else if (FBMove == MoveDir.SL)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.right, Time.deltaTime * Speed/2);
                if (senL.Hit)
                {
                    FBMove = MoveDir.None;
                }

                if (curMove == MoveDir.None)
                {
                    StartCoroutine(JumpsFollowAction(false));
                }
            }
            else if (FBMove == MoveDir.SR)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.right, Time.deltaTime * Speed/2);
                if (senR.Hit)
                {
                    FBMove = MoveDir.None;
                }

                if (curMove == MoveDir.None)
                {
                    StartCoroutine(JumpsFollowAction(false));
                }
            }
        }

        if (LRRotate != MoveDir.None && false)
        {
            
            mWang = transform.eulerAngles;
            if (LRRotate == MoveDir.Lt)
            {
                mWang = Vector3.MoveTowards(mWang, mWang + (Vector3.down*90f), 30f * Time.deltaTime * Speed);
                
            }
            else if (LRRotate == MoveDir.Rt)
            {
                mWang = Vector3.MoveTowards(mWang, mWang + (Vector3.up * 90f), 30f * Time.deltaTime * Speed);

            }
            transform.eulerAngles = mWang;

        }
    }

    #region FBLR Controls
    [SerializeField] MoveDir FBMove, LRRotate;

    public void ReceiveFBLR(MoveDir cmd, bool doAct)
    {
        if (curMove != MoveDir.None)
            return;

        if(cmd == MoveDir.Fwd )
        {
            if (doAct && !senF.Hit)
                FBMove = MoveDir.Fwd;
            else if (FBMove == MoveDir.Fwd)
                FBMove = MoveDir.None;
        }

        if (cmd == MoveDir.Bk)
        {
            if (doAct && !senB.Hit)
                FBMove = MoveDir.Bk;
            else if (FBMove == MoveDir.Bk)
                FBMove = MoveDir.None;
        }

        if (cmd == MoveDir.Lt)
        {
            if (doAct )
                LRRotate = MoveDir.Lt;
            else if (LRRotate == MoveDir.Lt)
                LRRotate = MoveDir.None;
        }

        if (cmd == MoveDir.Rt)
        {
            if (doAct )
                LRRotate = MoveDir.Rt;
            else if (LRRotate == MoveDir.Rt)
                LRRotate = MoveDir.None;
        }

        if (cmd == MoveDir.SL)
        {
            if (doAct && !senL.Hit)
                FBMove = MoveDir.SL;
            else if (FBMove == MoveDir.SL)
                FBMove = MoveDir.None;
        }

        if (cmd == MoveDir.SR)
        {
            if (doAct && !senR.Hit)
                FBMove = MoveDir.SR;
            else if (FBMove == MoveDir.SR)
                FBMove = MoveDir.None;
        }
    }
#endregion

}
