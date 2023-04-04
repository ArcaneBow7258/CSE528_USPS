using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILineRenderer : Graphic
{
    List<Vector2> points = new List<Vector2>();
    public float thickness = 10f;
    public static UILineRenderer Instance;
    protected override void Awake(){
        if(Instance == null){
            Instance = this;
        }
        else{
            Debug.Log("You have two UILineRenderers");
        }
    }
    protected override void OnPopulateMesh(VertexHelper vh){
        vh.Clear();
        for(int i = 0; i < points.Count;i++){
            Vector2 point = points[i];
            DrawVerticiesForPoint(point, vh);
        }
        for(int i = 0; i < points.Count-1;i++){
            int index = i*2;
            vh.AddTriangle(index+0, index +1, index +3);
            vh.AddTriangle(index +3, index +2, index +0);
        }
        
    }
    void DrawVerticiesForPoint(Vector2 point, VertexHelper vh){
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = Color.white;
        vertex.position = new Vector3(-thickness/2,0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);
        vertex.position = new Vector3(thickness/2,0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);
    }
    public void AddPoint(Vector2 pos){
        points.Add(pos);
    }
}
