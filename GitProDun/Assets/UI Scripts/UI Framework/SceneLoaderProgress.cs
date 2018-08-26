using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoaderProgress : MonoBehaviour {
    public Slider slider;
    public Text txtProgress;
    // Use this for initialization
    void Start () {
        LoadLevel(1);

    }
	


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine( LoadAsynchronously(sceneIndex) );
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float prog = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = prog;
            txtProgress.text = Mathf.RoundToInt(prog * 100f) + " %";
            yield return null;

        }
    }
}
