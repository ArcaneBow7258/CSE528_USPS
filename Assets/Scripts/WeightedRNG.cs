using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class dupeCompare:IComparer<float>{
    #region IComparer<Tkey> Members
    public int Compare(float x, float y)
    {
        int result = x.CompareTo(y);
        if(result == 0){
            return 1;
        }else{
            return result;
        }
    }
    #endregion
}
[Serializable]
public struct GameObject_Chance{
    public GameObject prefab;
    public int chance;
}
public class weightedRNG{
    private float totalChance = 0; 
    private SortedList<float, GameObject> table = new SortedList<float, GameObject>(new dupeCompare()); // relative weights please
    public weightedRNG( List<float> c, List<GameObject> g){
        if(c.Count != g.Count){
            throw new UnityException("You do not have a room for every chance or a chance for each room");
        }else{
            for(int i = 0; i < c.Count; i++)
            {
                table.Add(c[i],g[i]);
                totalChance += c[i];
            }
        }
    
    }
    public weightedRNG(List<GameObject_Chance> t){
        foreach(var i in t){
            totalChance += i.chance;
            table.Add(i.chance,i.prefab);
        }

    
    }
    public void Add(float c, GameObject g){
        table.Add(c,g);
        totalChance += c;
    }
    public GameObject gen(){
        float random = UnityEngine.Random.Range(0,totalChance);
        //Debug.Log("Init random " + random);
        GameObject room = null;
        foreach(var entry in table){
            if(random <= entry.Key){
                room = entry.Value;
                break;
            }else{
                random -= entry.Key;
                //Debug.Log("next random" + totalChance);
            }
        }
        return room;
    }
}
