using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class scoLevels : ScriptableObject
{
    public int TotalLevel=0;
    [System.Serializable]
    public struct LevelTuple
    {
        public int Level;
        public int Maze;
        public int Collectible;
    }

    public List<LevelTuple> levelList;

    public void AddLevel(int plevel, int pcollectible)
    {
        LevelTuple lt = new LevelTuple();
        lt.Level = TotalLevel;
        lt.Maze = plevel;
        lt.Collectible = pcollectible;
        TotalLevel++;

        levelList.Add(lt);
    }
}
