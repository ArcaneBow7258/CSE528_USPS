using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
using System;

[CustomNodeGraphEditor(typeof(SkillGraph))]
public class SkillGraphEditor : NodeGraphEditor
{
    private SkillGraph graph;
    private Node lastNode;
    private UnityEngine.Object[] lastGroup;
    private Node referenceNode;
    private UnityEngine.Object[] refG;
    private float x;
    private float y;
    
    public override void OnGUI()
    {
        
        
        base.OnGUI();
        
        if(graph == null){
            graph = target as SkillGraph;
        }
        GUILayout.BeginArea(new Rect(10,10,200,500));
        if(graph.highest != null){
            foreach(STATTYPE type in graph.highest.Keys){
                EditorGUILayout.LabelField(type + " " +((int)type*1000 +  graph.highest[type]).ToString());
            }
        }

        GUILayout.EndArea();
        /*
        GUI.backgroundColor = Color.red;
        Node selected = (Node)Selection.activeObject;
        if(selected != null){
            lastNode =selected;
        }

        UnityEngine.Object[] sel = Selection.objects;
        if(sel != null){
            lastGroup =sel;
        }
        
        
        GUILayout.BeginArea(new Rect(10,10,200,300));
        if(GUILayout.Button("Set Ref", GUILayout.Width(50)) ){
            referenceNode = lastNode;
        }
        if(referenceNode != null){
            EditorGUILayout.LabelField(referenceNode.name);
        }
        if(GUILayout.Button("Set Group", GUILayout.Width(50)) ){
            refG = sel;
        }
        if(referenceNode != null){
            EditorGUILayout.LabelField(referenceNode.name);
        }
        if(refG != null){
            foreach(Node n in lastGroup){
                EditorGUILayout.LabelField("Group: " + n.position);
            }
        }
        EditorGUILayout.BeginHorizontal();
        x =EditorGUILayout.FloatField(x, GUILayout.Width(50));
        if(GUILayout.Button("Set X", GUILayout.Width(50)) ){
            Debug.Log("press");
            foreach(Node n in sel){
                n.position.x = (x);
                Debug.Log("Test");
            }
            
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        y= EditorGUILayout.FloatField(y, GUILayout.Width(50));
        if(GUILayout.Button("Set Y", GUILayout.Width(50)) ){
            foreach(Node n  in sel){
                n.position.y = (y);
            }
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.EndArea();



    */
        serializedObject.Update();
    }
}
