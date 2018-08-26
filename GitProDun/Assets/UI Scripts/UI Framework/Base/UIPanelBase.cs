using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Every UI Panel/Screen Should Derive from this.
/// </summary>
[RequireComponent(typeof(ExitAnim))]
[RequireComponent(typeof(EntryAnim))]
public class UIPanelBase : MonoBehaviour {
    public EntryAnim aEntry;
    public ExitAnim aExit;

    
    public void Reset()
    {
        aEntry = GetComponent<EntryAnim>();
        aExit = GetComponent<ExitAnim>();
    }

    public void OnValidate()
    {
        if(aEntry.Fade.Applyon == animCurve.Element.CanvasGroup ||
           aExit.Fade.Applyon == animCurve.Element.CanvasGroup)
        {
            CanvasGroup cg = GetComponent<CanvasGroup>();
            if (cg == null)
                gameObject.AddComponent<CanvasGroup>();
        }
    }


}
