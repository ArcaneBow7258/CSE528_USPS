using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
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
    public float chance;
    public float value;
    public float limit;
    public void remove(){
        limit -= 1;
    }
}
public class weightedRNG{
    private float totalChance = 0; 
    private float currentvalue = 0;
    private List<GameObject_Chance> table = new List<GameObject_Chance>(); // relative weights please
   
    public weightedRNG(List<GameObject_Chance> t){
        table = t;
        totalChance = table.Sum(x => x.chance);
    }
    public weightedRNG(List<GameObject_Chance> t, float totalValue){
        currentvalue = totalValue;
        totalChance = table.Sum(x => x.chance);
        table = t;
    }
    public GameObject gen(){
        float random = UnityEngine.Random.Range(0,totalChance);
        //Debug.Log("Init random " + random);
        GameObject room = null;
        foreach(var entry in table){
            if(random <= entry.chance){
                room = entry.prefab;
                break;
            }else{
                random -= entry.chance;
                //Debug.Log("next random" + totalChance);
            }
        }
        return room;
    }
    public GameObject gen(bool limited){
        float random;
        //Debug.Log("Init random " + random);
        GameObject room = null;
        while(room != null){
           random = UnityEngine.Random.Range(0,totalChance);
            foreach(GameObject_Chance entry in table.Where(e => e.limit != 0)){
                if(random <= entry.chance){
                    if(entry.limit > 0){
                        entry.remove();
                    }
                    room = entry.prefab;
                    
                    break;
                }else{
                    random -= entry.chance;
                    //Debug.Log("next random" + totalChance);
                }

           } 
        }
        return room;

    }
    
}
