using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour {
    public static int Counter = 0;
    GameObject go;
    public ProcDung Ui;
    public void Awake()
    {
        Counter++;

    }

    public void Start()
    {
            Ui.disp.text = "Remaining: " + Counter;

    }
    void OnTriggerEnter(Collider colon)
    {
        go = colon.gameObject;
        if(go.layer == 8)
            Destroy(gameObject);
    }

    public void OnDestroy()
    {
        Counter--;
        if (Ui.disp != null)
        {
            Ui.disp.text = "Remaining: " + Counter;
            if (Counter == 0 )
            {
                Ui.disp.text = "Level Completed";
                IngameUI.Instance.LevelCompleted();
                //go.GetComponent<WildTraverse>().mAuto = false;
            }
        }
    }
}
