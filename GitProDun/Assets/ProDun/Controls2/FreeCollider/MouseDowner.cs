using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDowner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,IPointerExitHandler {
    public CBFreeControls.MoveDir MyActionType;
    [SerializeField] bool flgOn=false;
    public CBFreeControls ControlCentre;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!flgOn)
        {
            ControlCentre.ReceiveFBLR(MyActionType, true);
            flgOn =  true;
        }
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        //if (!flgOn)
        //{
        //    ControlCentre.ReceiveFBLR(MyActionType, true);
        //    flgOn = true;
        //}
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        //if (flgOn)
        //{
        //    ControlCentre.ReceiveFBLR(MyActionType, false);
        //    flgOn = false;
        //}

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (flgOn)
        {
            ControlCentre.ReceiveFBLR(MyActionType, false);
            flgOn =  false;
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	



}
