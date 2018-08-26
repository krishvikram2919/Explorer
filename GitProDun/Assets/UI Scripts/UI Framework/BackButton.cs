using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public interface IBackAction{
    void BackAction();
}

public class BackButton : MonoBehaviour {
    [Header("Use for Cascaded Screen")]
    public BackButton prevBackButton;

    public enum type{Scene,BackPanel,Popup,None};
    [Header("Back Buton Config")]
    public type BackType;
    [HideInInspector] public string sceneName;
    [HideInInspector] public GameObject Panel;
    [HideInInspector] public GameObject Popup;

    IBackAction backAction;

    //
    public void Start()
    {
        backAction = GetComponent<IBackAction>();
        if (backAction == null)
            backAction = new EmptyBack();
    }
    //Input
    void Update () {
	
		if ( Input.GetKeyUp ( KeyCode.Escape) ){
				OnBackButton();
		}

	}

    //Disables Prev Screen's Back Button to avoid Overlapping cascade Back
	void OnEnable(){
		if(prevBackButton != null)
			prevBackButton.enabled=false;
	}


    /// <summary>
    /// Also Exit Popup's No Button Call(None).
    /// </summary>
	public void OnBackButton(){

        //Exit this Panel to prev. (Non-Cascaded Previous Screen)
        if (BackType == type.BackPanel)
        {
            Panel.SetActive(true);
            this.gameObject.SetActive(false);

        //Exit this Panel to prev. (Cascaded Previous Screen)
        }else if (BackType == type.None)
        {
            if (prevBackButton != null)
                prevBackButton.enabled = true;
            this.gameObject.SetActive(false);

        }else if (BackType == type.Scene)
        {
            SceneManager.LoadScene(sceneName);

        //Open a Popup on Current. (Cascade Next Popup/Screen)
        }else if (BackType == type.Popup)
        {
            Popup.SetActive(true);
            Popup.GetComponent<BackButton>().prevBackButton = this;
            this.enabled = false;
        }


		

   
        backAction.BackAction();
	}


    //Exit Popup's Yes Button Calls this Function
	public void Quit(){
		Application.Quit();
	}


	void OnValidate(){

		if(BackType== type.BackPanel){
			if( Panel == null){
				Debug.LogError(gameObject.name+ ": Back Panel is Missing.");
			}
		}else if ( BackType == type.Scene){
			if( sceneName == ""){
				Debug.LogError(gameObject.name+ ": Scene Name is Missing");
			}
		}else if ( BackType == type.Popup){
			if( Popup == null){
				Debug.LogError(gameObject.name+ ": Exit Panel reference is Missing");
			}
		}
	}

}

public class EmptyBack : IBackAction
{
    public void BackAction()
    {
    }
}