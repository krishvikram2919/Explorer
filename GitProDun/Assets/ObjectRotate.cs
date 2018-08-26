using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRotate : MonoBehaviour,IPointerDownHandler {
    public Transform epicentre;
    public Transform obj;
    [Range(-1,1)]
    public int direction;

    public float speed = 360f;
    static bool flgWorking=false;

    Vector3 rovector;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!flgWorking)
            StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        flgWorking = true;
        float amount = 0f;
        float quanta = 0f;
        while (true)
        {
            quanta = speed * Time.deltaTime;
            amount += quanta;
            if (amount > 90f)
                quanta = quanta - (amount - 90f);
            quanta *= direction;
            obj.RotateAround(epicentre.position, Vector3.up, quanta );

            yield return null;
            if ( amount >= 90f)
            {
                flgWorking = false;
                rovector = obj.transform.localPosition;
                rovector.x = Mathf.RoundToInt(rovector.x);
                rovector.y = Mathf.RoundToInt(rovector.y);
                rovector.z = Mathf.RoundToInt(rovector.z);
                obj.transform.localPosition = rovector;

                rovector = obj.transform.localEulerAngles;
                rovector.x = Mathf.RoundToInt(rovector.x);
                rovector.y = Mathf.RoundToInt(rovector.y);
                rovector.z = Mathf.RoundToInt(rovector.z);
                obj.transform.localEulerAngles = rovector;

                break;
            }

        }
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
