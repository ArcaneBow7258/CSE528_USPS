using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
public class NW_TileGenerator : NetworkBehaviour
{
    
    public Vector2 offset;

    public Vector2 size;
    public int startPos = 0;
    public int iterations = 20;
    public List<GameObject_Chance> rooms;
    private weightedRNG gen;
    List<Cell> board;
    private class Cell{
        public bool visted = false;
        public RoomStatus status = new RoomStatus();
    }
    void Awake(){
        gen = new weightedRNG(rooms);
    }
    void Start(){
        
        
    }
    public override void OnNetworkSpawn(){
        base.OnNetworkSpawn();
        if(IsServer) GridGenerator();
    }
    [ContextMenu("Regen")]
    public void Regenerate(){
        while(transform.childCount > 0){
            transform.GetChild(0).GetComponent<NetworkObject>().Despawn(false);
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        GridGenerator();
    }

    void RoomGenerator(){
        Debug.Log("Room start");
        for(int i = 0; i < size.x; i++){
            for(int j=0; j < size.y; j++){
                Cell currentCell = board[((int)(i +( j * size.x)))];
                GameObject choice = gen.gen();
                
                GameObject r = Instantiate(choice, new Vector3(i* offset.x+this.transform.position.x, 0, -j*offset.y + this.transform.position.y),Quaternion.identity, transform);
                //GameObject r = Instantiate(choice, new Vector3(i* offset.x+this.transform.position.x, 0, -j*offset.y + this.transform.position.y),Quaternion.identity, transform);
                NetworkObject n = r.GetComponent<NetworkObject>();
                
                //n.gameObject.SetActive(false);
                n.gameObject.GetComponent<NW_RoomBehavior>().UpdateRoom(currentCell.status);
                n.Spawn();
                n.TrySetParent(transform);
                //update room
            }

        }

    }
    void GridGenerator(){
        board = new List<Cell>();
        for(int i = 0; i < size.x; i++){
            for(int j = 0; j < size.y; j++){
                board.Add(new Cell());
            }

        }
        int currentCell = startPos;
        int distance = 0;
        Stack<int> path = new Stack<int>();
        int k = 0;
        while(k < iterations){
            k++;
            distance++;
            board[currentCell].visted = true;
            board[currentCell].status.distance = distance;
            List<int> neighbors = CheckNeighbors(currentCell);
            if(neighbors.Count ==0){
                if(path.Count==0){
                    break;
                }
                else{
                    currentCell = path.Pop();
                    distance--;
                }
            }else{
                path.Push(currentCell);
                distance++;
                int newCell = neighbors[UnityEngine.Random.Range(0,neighbors.Count)];

                if (newCell > currentCell){
                    if(newCell -1 == currentCell){
                        board[currentCell].status.side[2] = true;
                        //set wall = true;
                        currentCell = newCell;
                        board[currentCell].status.side[3] = true;
                        //set back wall is aslo true
                    }else{
                        //setwall = true
                         board[currentCell].status.side[1] = true;
                        currentCell = newCell;
                         board[currentCell].status.side[0] = true;
                    }   
                }else{
                    if(newCell + 1 == currentCell){
                        //left
                        board[currentCell].status.side[3] = true;
                        currentCell = newCell;
                        board[currentCell].status.side[2] = true;
                        //right
                    }else{
                        board[currentCell].status.side[0] = true;
                        currentCell = newCell;
                        board[currentCell].status.side[1] = true;
                    }
                }
            }
        }
        RoomGenerator();
    }
    List<int> CheckNeighbors(int cell){
        List<int> n = new List<int>();
        if(cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visted){
                n.Add(Mathf.FloorToInt(cell-size.x));

        }
        if(cell + size.x < board.Count && !board[Mathf.FloorToInt(cell+size.x)].visted){
                n.Add(Mathf.FloorToInt(cell+size.x));

        }
        if((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell+1)].visted){
                n.Add(Mathf.FloorToInt(cell+1));

        }
        if(cell % size.x != 0 && !board[Mathf.FloorToInt(cell-1)].visted){
                n.Add(Mathf.FloorToInt(cell-1));

        }
        return n;

    }
}
