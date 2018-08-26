using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class animCurve{
    public enum Element { CanvasGroup,Image };
    public bool Enable;
    public AnimationCurve Curve;
    public bool Loop;
    float duration;

    [HideInInspector]
    public float startValue = 0f;

    [HideInInspector]
    public float timevalue;

    public float Duration
    {
        get
        {
            duration = Curve[Curve.length - 1].time;
            return duration;
        }
    }

    public Element Applyon;
    [HideInInspector] public Image image;
    [HideInInspector] public CanvasGroup canvasgroup;
}

[System.Serializable]
public class animCurve2D{
	public bool Enable;
	public bool IsCurveRelative=true;
	public AnimationCurve CurveX, CurveY;
	public bool Loop;
	public Vector2 Factor;
	float duration;


	[HideInInspector]
	public Vector3 startValue=Vector3.zero;
	[HideInInspector]
	public float timevalue;

	public float Duration {
		get {
			duration = Mathf.Max( CurveX[CurveX.length-1].time, CurveY[CurveY.length-1].time);
			return duration;
		}
	}
}

[System.Serializable]
public class animCurve3D{
	public bool Enable;
	public bool IsCurveRelative=true;
	public AnimationCurve CurveX, CurveY, CurveZ;
	public bool Loop;
	public Vector3 Factor;
	float duration;
	
	[HideInInspector]
	public Vector3 startValue=Vector3.zero;
	[HideInInspector]
	public float timevalue;
	
	public float Duration {
		get {
			duration = Mathf.Max( CurveX[CurveX.length-1].time, CurveY[CurveY.length-1].time);
			duration = Mathf.Max( duration, CurveZ[CurveZ.length-1].time);
			return duration;
		}
	}
}


public class UITransformationWithCurves : MonoBehaviour {
	public float Speed=1;
	protected bool PlayOnStart=false;
    protected bool ReplayOnEnable =false;
    protected bool DisableAfterAnim = false;
	public animCurve2D Position;
	public animCurve3D Rotation;
	public animCurve2D Scale;

	public animCurve Fade;
	protected Vector3 initPos, initRot, initScale;
	protected float duration;

	public float Duration {
		get {
			return duration;
		}
	}

	protected RectTransform myRect;

	protected void iAwake () {
	
		duration=0;
		if(Position.Enable)
			duration = (duration < Position.Duration) ? Position.Duration :duration;
		if(Rotation.Enable)
			duration = (duration < Rotation.Duration) ? Rotation.Duration :duration;
		if(Scale.Enable)
			duration = (duration < Scale.Duration) ? Scale.Duration :duration;
        if (Fade.Enable){
            duration = (duration < Fade.Duration) ? Fade.Duration : duration;

            if (Fade.Applyon == animCurve.Element.CanvasGroup)
                Fade.canvasgroup = GetComponent<CanvasGroup>();
            else
                Fade.image = GetComponent<Image>();
        }
        myRect = gameObject.GetComponent<RectTransform>();

        if (Position.IsCurveRelative)
            Position.startValue = myRect.localPosition;
        if (Rotation.IsCurveRelative)
            Rotation.startValue = myRect.localEulerAngles;
        if (Scale.IsCurveRelative)
            Scale.startValue = myRect.localScale;

        initPos = myRect.localPosition;
        initRot = myRect.localEulerAngles;
        initScale = myRect.localScale;
    }

	protected void iStart(){



	}


	protected void iOnEnable(){
		if(!ReplayOnEnable)
			return;

        myRect.localPosition=initPos;
		myRect.localEulerAngles=initRot;
		myRect.localScale = initScale;

		Animate();
			
	}
 
 

	protected void Animate () {
	
		if(Position.Enable)
			StartCoroutine( TransformPosition() );
		if(Rotation.Enable)
			StartCoroutine( TransformRotation() );
		if(Scale.Enable)
			StartCoroutine( TransformScale() );
        if (Fade.Enable){
            if (Fade.Applyon == animCurve.Element.CanvasGroup)
                StartCoroutine(FadeCanvas());
            else
                StartCoroutine(FadeUI());
        }
		
	}

	#region Transformations
	IEnumerator TransformPosition(){
		Position.timevalue=0f;
		Vector3 offset= Vector3.zero;
		
		while( Position.timevalue <= Position.Duration || Position.Loop){
			offset.x = Position.CurveX.Evaluate(Position.timevalue)*Position.Factor.x;
			offset.y = Position.CurveY.Evaluate(Position.timevalue)*Position.Factor.y;
			myRect.localPosition = Position.startValue+ offset ;
			yield return null;
			Position.timevalue += Time.deltaTime * Speed;

		}

		Position.timevalue = Position.Duration;
		offset.x = Position.CurveX.Evaluate(Position.timevalue)*Position.Factor.x;
		offset.y = Position.CurveY.Evaluate(Position.timevalue)*Position.Factor.y;
		myRect.localPosition = Position.startValue+ offset ;
        DisableElement();

    }

    IEnumerator TransformRotation(){
		Rotation.timevalue=0f;
		Vector3 offset= Vector3.zero;
		
		while( Rotation.timevalue <= Rotation.Duration || Rotation.Loop)
        {
			offset.x = Rotation.CurveX.Evaluate(Rotation.timevalue)*Rotation.Factor.x;
			offset.y = Rotation.CurveY.Evaluate(Rotation.timevalue)*Rotation.Factor.y;
			offset.z = Rotation.CurveZ.Evaluate(Rotation.timevalue)*Rotation.Factor.z;
			myRect.localRotation = Quaternion.Euler (Rotation.startValue+ offset) ;
			yield return null;
			Rotation.timevalue += Time.deltaTime * Speed;
			
		}

		Rotation.timevalue = Rotation.Duration;
		offset.x = Rotation.CurveX.Evaluate(Rotation.timevalue)*Rotation.Factor.x;
		offset.y = Rotation.CurveY.Evaluate(Rotation.timevalue)*Rotation.Factor.y;
		offset.z = Rotation.CurveZ.Evaluate(Rotation.timevalue)*Rotation.Factor.z;
		myRect.localRotation = Quaternion.Euler (Rotation.startValue+ offset) ;
        DisableElement();

    }

    IEnumerator TransformScale(){
		Scale.timevalue=0f;
		Vector3 offset= Vector3.zero;
		
		while( Scale.timevalue <= Scale.Duration || Scale.Loop )
        {
			offset.x = Scale.CurveX.Evaluate(Scale.timevalue)*Scale.Factor.x;
			offset.y = Scale.CurveY.Evaluate(Scale.timevalue)*Scale.Factor.y;
			myRect.localScale = Scale.startValue+ offset ;
			yield return null;
			Scale.timevalue += Time.deltaTime * Speed;
			
		}

		Scale.timevalue = Scale.Duration;
		offset.x = Scale.CurveX.Evaluate(Scale.timevalue)*Scale.Factor.x;
		offset.y = Scale.CurveY.Evaluate(Scale.timevalue)*Scale.Factor.y;
		myRect.localScale = Scale.startValue+ offset ;
        DisableElement();

    }

    IEnumerator FadeUI()
    {
        Fade.timevalue = 0f;
        float offset = 0f;
        Color clr = Fade.image.color;

		Fade.startValue=0f;
        while (Fade.timevalue <= Fade.Duration || Fade.Loop)
        {
            offset = Fade.Curve.Evaluate(Fade.timevalue) ;

            clr.a = Fade.startValue + offset;
            Fade.image.color = clr;
            yield return null;
            Fade.timevalue += Time.deltaTime * Speed;

        }

        Fade.timevalue = Fade.Duration;
        offset = Fade.Curve.Evaluate(Fade.timevalue);
        clr.a = Fade.startValue + offset;
        Fade.image.color = clr;
        DisableElement();

    }

    IEnumerator FadeCanvas()
    {
        Fade.timevalue = 0f;
        float offset = 0f;
		Fade.startValue=0f;

        while (Fade.timevalue <= Fade.Duration || Fade.Loop)
        {
            offset = Fade.Curve.Evaluate(Fade.timevalue);

            Fade.canvasgroup.alpha = Fade.startValue + offset;
            yield return null;
            Fade.timevalue += Time.deltaTime * Speed;

        }

        Fade.timevalue = Fade.Duration;
        offset = Fade.Curve.Evaluate(Fade.timevalue);
        Fade.canvasgroup.alpha = Fade.startValue + offset;
        DisableElement();
    }


    protected void DisableElement()
    {
        if (DisableAfterAnim)
            gameObject.SetActive(false);
   }

	#endregion
}
