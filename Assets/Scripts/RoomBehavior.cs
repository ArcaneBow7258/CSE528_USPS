using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RoomStatus{
    public bool[] side = {false, false, false, false};//corresponds to if  door should be present or not.
    public int distance = 0; //distance from starting room

}
public class RoomBehavior : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up 1 -Down 2 - Right 3- Left
    public GameObject[] doors;
    public GameObject[] spawned;
    public GameObject[] spawnPoints;
    public float[] chances;
    public float noChance;
    private weightedRNG roomgen;
    void Awake(){
        roomgen = new weightedRNG(chances, spawned);
        roomgen.Add(noChance, null);
    }
    public void UpdateRoom(RoomStatus stat)
    {
        bool access = false;
        for (int i = 0; i < stat.side.Length; i++)
        {
            access = access || stat.side[i];
            doors[i].SetActive(stat.side[i]);
            walls[i].SetActive(!stat.side[i]);
        }
        if(!access){
            Destroy(gameObject);
        }
        foreach(var sp in spawnPoints){
            GameObject spawned = roomgen.gen();
            if(spawned != null){
                Instantiate(spawned, sp.transform);
            }
            
        }
    }
}