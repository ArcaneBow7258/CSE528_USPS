using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XNode;
using XNodeEditor;
[CustomNodeEditor(typeof(PassiveNode))]
public class PassiveNodeEditor : NodeEditor {
    private PassiveNode node;
    private bool collapse = false;

    private bool showDrop = true;
    public override void OnBodyGUI() {
        //GUI.contentColor = Color.black;
        
        if (node == null) node = target as PassiveNode;
        //Rename(node.skillName);
        // Update serialized object's representation
        serializedObject.Update();
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("stats"));
        /*
        Color prev = GUI.backgroundColor;
        GUI.backgroundColor = Color.red;
        if(GUILayout.Button("Collapse")){
            collapse = !collapse;
        }
        GUI.backgroundColor = prev;
        if(collapse){
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
            
        }*/
        
        showDrop = EditorGUILayout.BeginFoldoutHeaderGroup(showDrop, "Settings");
        if(showDrop){
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("id"));
            //NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));
            node.icon= (Sprite)EditorGUILayout.ObjectField("Sprite",node.icon, typeof(Sprite),false);
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("skillName"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("skillDesc"));
        }else{
            //GUILayout.Box(node.icon.texture, GUILayout.Width(40),  GUILayout.Height(40));
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
       
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exits"));
        
        // Apply property modifications
        serializedObject.ApplyModifiedProperties();
    }
    public override int GetWidth(){
        if(showDrop){
            return 200;
        }else{
            return 200;
        }
    }
}
