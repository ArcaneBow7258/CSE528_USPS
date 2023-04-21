using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//https://www.youtube.com/watch?v=--LB7URk60A
public class UILineRenderer : Graphic
{
    public List<Vector2> points = new List<Vector2>();
    public List<Color32> colors = new List<Color32>();
    public float thickness = 10f;
    protected override void Awake(){
    
    }
    protected override void OnPopulateMesh(VertexHelper vh){
        vh.Clear();
        float angle = 0;
        for(int i = 0; i < points.Count;i++){
            Vector2 point = points[i];
            if (i<points.Count - 1){
                angle = GetAngle(points[i],points[i+1]) + 45f;
            }
            //Debug.Log(points.Count + " - " + colors.Count);
            DrawVerticiesForPoint(point, vh,angle, colors[i]);
        }
        for(int i = 0; i < points.Count-1;i++){
            int index = i*2;
            vh.AddTriangle(index+0, index +1, index +3);
            vh.AddTriangle(index +3, index +2, index +0);
        }
        
    }
    void DrawVerticiesForPoint(Vector2 point, VertexHelper vh, float angle, Color32 c){
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = c;
        vertex.position = Quaternion.Euler(0,0,angle) *  new Vector3(-thickness/2,0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);
        vertex.position = Quaternion.Euler(0,0,angle) * new Vector3(thickness/2,0);
        vertex.position += new Vector3(point.x, point.y);
        vh.AddVert(vertex);
    }
    public void AddPoint(Vector2 pos, Color c){
        points.Add(pos);
        colors.Add(c);
        
    }
    public float GetAngle(Vector2 me, Vector2 target){
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }
    public void ChangeColor(int index, Color32 color){
       
        colors[index] = color;
        
        
        
    }
    public void Update(){
        SetVerticesDirty();
    
         
        
    }
}
