using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcDung : MonoBehaviour {
    public GameObject pfTile;
    public GameObject pfCollectible;

    public const int max = 10;
    public const int tier = 4;
    [SerializeField]public static int[, ,] bitmap = new int[max, tier, max];

    public int SeedLevel;
    public int SeedCollect;

    GameObject RootLevel;
    GameObject RootCollect;
    public GameObject goPlayer;
    public Text disp;

    public Light liArea, liPlayer;
    public scoLevels Levelinfo;
    // Use this for initialization
    void Start () {

        
        //CreateLevel();
        //SetCollectible();

    }

    public void InitialiseLevel()
    {
        //SeedLevel = Random.Range(0, 9999);
        //SeedCollect = Random.Range(0, 9999);
        goPlayer.SetActive(false);
        goPlayer.GetComponent<CBFreeControls>().Start();
        goPlayer.SetActive(true);
        Vector3 clr = new Vector3(Random.Range(0f, 1.1f), Random.Range(0f, 1.1f), Random.Range(0f, 1.1f));

        liArea.color = new Color(clr.x, clr.y, clr.z);
        liPlayer.color = new Color(1f-clr.x, 1f-clr.y, 1f-clr.z);
        CreateLevel();
    }
    public void CreateLevel()
    {
        Vector3 pos;

        Random.InitState(SeedLevel);

        for (int i = 0; i < tier; i++)
        {
            for (int j = 0; j < max; j++)
            {
                for (int k = 0; k < max; k++)
                    bitmap[j, i, k] = 0;
            }
        }

        Destroy(RootLevel);
        RootLevel = new GameObject();
        RootLevel.name = "Root Level";
        GameObject go;
        Color clr = Color.white;
        for (int i = 0; i < tier-1; i++)
        {
            pos = Vector3.zero;
            pos.y = i;
            clr = new Color(1f / (i+1f), 1f / (i + 1f), 1f / (i + 1f));
            go = new GameObject();
            go.transform.parent = RootLevel.transform;
            go.name = "Level " + i;
            for (int j = i; j < max - i; j++)
            {
                pos.z = j;
                for (int k = i; k < max - i; k++)
                {
                    pos.x = k;
                    if ((i < 1) || (Random.Range(2, 99) % 2 == 0))
                    {
                        bitmap[k, i, j] = 1;
                        GameObject g = Instantiate(pfTile, pos, Quaternion.identity) as GameObject;
                        g.transform.parent = go.transform;
                        g.GetComponent<Cube>().loc = new Vector3(k, i, j);
                        g.GetComponent<Renderer>().material.color = clr;
                    }
                }
            }

        }

        SetCollectible();
    }

    public void SetCollectible()
    {
        GameObject go;
        Vector3 pos;
        Random.InitState(SeedCollect);

        Destroy(RootCollect);
        go = new GameObject();
        RootCollect = go;
        go.name = "Collectible";


        for (int i = 0; i < tier; i++)
        {
            for (int j = 0; j < max; j++)
            {
                for (int k = 0; k < max; k++)
                {
                    if(bitmap[j, i, k] ==2)
                        bitmap[j, i, k] = 0;
                }
            }
        }

        for (int i = 1; i < tier; i++)
        {
            pos = Vector3.zero;
            pos.y = i;

            for (int j = i; j < max - i; j++)
            {
                pos.z = j;
                for (int k = i; k < max - i; k++)
                {
                    pos.x = k;
                    
                    if ( (Random.Range(2, 99) % 5 == 0))
                    {
                        bool flg = false;

                        if(i+1 < tier)
                         flg = flg || (bitmap[k, i+1, j] == 0);
                        if(k+1 <max)
                         flg = flg || (bitmap[k+1, i, j] == 0);
                        if(k-1 >=0)
                         flg = flg || (bitmap[k-1, i, j] == 0);
                        if(j+1 < tier)
                         flg = flg || (bitmap[k, i, j+1] == 0);
                        if(j-1 >=0)
                         flg = flg || (bitmap[k, i, j-1] == 0);

                        flg = flg && (bitmap[k, i, j] == 0);
                        if (flg)
                        {
                            bitmap[k, i, j] = 2;
                            GameObject g = Instantiate(pfCollectible, pos, Quaternion.identity) as GameObject;
                            g.transform.parent = go.transform;
                            g.transform.localScale *= 0.75f;
                            g.GetComponent<Cube>().loc = new Vector3(k, i, j);
                            g.GetComponent<BoxCollider>().isTrigger = true;
                            g.AddComponent<Killer>();
                            g.GetComponent<Killer>().Ui = this;
                        }
                    }
                }
            }

        }



    }

    public void SaveLevel()
    {
        Levelinfo.AddLevel(SeedLevel, SeedCollect);
    }
}
