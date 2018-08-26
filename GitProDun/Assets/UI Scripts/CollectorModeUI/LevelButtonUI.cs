using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour {
    public Text txtScore, txtLevel;
    public GameObject[] Stars;

    public void SetStars(int count)
    {
        count = (count > 3) ? 3 : 1;

        for (int i = 0; i < count; i++)
            Stars[i].SetActive(true);

    }
}
