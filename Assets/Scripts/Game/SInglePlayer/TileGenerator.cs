/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    
    public Vector2 offset;

    public Vector2 size;
    public int startPos = 0;
    public int iterations = 10;
    public List<GameObject> rooms;
    public List<float> chances;
    private weightedRNG gen;
    List<Cell> board;
    private class Cell{
        public bool visted = false;
        public RoomStatus status = new RoomStatus();

    }
    void Awake(){
        gen = new weightedRNG(chances, rooms);
    }
    void Start(){
        GridGenerator();
    }


    void RoomGenerator(){
        
        for(int i = 0; i < size.x; i++){
            for(int j=0; j < size.y; j++){
                Cell currentCell = board[((int)(i +( j * size.x)))];
                GameObject choice = gen.gen();

                GameObject r = Instantiate(choice, new Vector3(i* offset.x+this.transform.position.x, 0, -j*offset.y + this.transform.position.y),Quaternion.identity, transform);
                r.GetComponent<RoomBehavior>().UpdateRoom(currentCell.status);
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
                int newCell = neighbors[Random.Range(0,neighbors.Count)];

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
*/