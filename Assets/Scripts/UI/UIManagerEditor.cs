using System.Collections;
using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    SerializedProperty backButton;
    SerializedProperty u_skilltree;
    SerializedProperty u_skillSelect;
    SerializedProperty u_mainmenu;
    //entire lobby interface
    SerializedProperty u_lobby;
    //large lobby 
    SerializedProperty u_joinLobby;
    //create lobby
    SerializedProperty lname;
    SerializedProperty size;
    SerializedProperty visibility;
    SerializedProperty difficulty;
    SerializedProperty length;
    SerializedProperty joinCode;
    //join lobby
    SerializedProperty lobbyList;
    SerializedProperty lobbyPanel;

    //while in lobby
    SerializedProperty u_inLobby;
    //Game Lobby Ui
    SerializedProperty playerList;
    SerializedProperty playerPanel;
    SerializedProperty gameInfo;
    bool createLobbyGroup = false;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(u_mainmenu);
        EditorGUILayout.PropertyField(u_skilltree);
        EditorGUILayout.PropertyField(u_skillSelect);
        //entire lobby interface
        EditorGUILayout.PropertyField(u_lobby);
        //large lobby 
        EditorGUILayout.PropertyField(u_joinLobby);
        //create lobby
        createLobbyGroup = EditorGUILayout.BeginFoldoutHeaderGroup(createLobbyGroup, "Create Lobby");
        if(createLobbyGroup){
            EditorGUILayout.PropertyField(lname);
            EditorGUILayout.PropertyField(size);
            EditorGUILayout.PropertyField(visibility);
            EditorGUILayout.PropertyField(difficulty);
            EditorGUILayout.PropertyField(length);
            EditorGUILayout.PropertyField(joinCode);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        EditorGUILayout.Space(5);
        //lobby listing
        EditorGUILayout.PropertyField(lobbyList);
        EditorGUILayout.PropertyField(lobbyPanel);
        EditorGUILayout.Space(5);
        
        //while in lobby
        EditorGUILayout.PropertyField(u_inLobby);
        //Game Lobby Ui
        EditorGUILayout.PropertyField(playerList);
        EditorGUILayout.PropertyField(playerPanel);
        EditorGUILayout.PropertyField(gameInfo);
        EditorGUILayout.Space(5);
        EditorGUILayout.PropertyField(backButton);
        serializedObject.ApplyModifiedProperties();
        
    }
    private void OnEnable(){
        backButton=serializedObject.FindProperty("backButton");
        u_skilltree=serializedObject.FindProperty("u_skilltree");
        u_mainmenu=serializedObject.FindProperty("u_mainmenu");
        u_skillSelect=serializedObject.FindProperty("u_skillSelect");
        //entire lobby interface
        u_lobby=serializedObject.FindProperty("u_lobby");
        //large lobby 
        u_joinLobby=serializedObject.FindProperty("u_joinLobby");
        //create lobby
        lname=serializedObject.FindProperty("lname");
        size=serializedObject.FindProperty("size");
        visibility=serializedObject.FindProperty("visibility");
        difficulty=serializedObject.FindProperty("difficulty");
        length=serializedObject.FindProperty("length");
        joinCode=serializedObject.FindProperty("joinCode");
        //join lobby
        lobbyList=serializedObject.FindProperty("lobbyList");
        lobbyPanel=serializedObject.FindProperty("lobbyPanel");
        

        //while in lobbyu_inLobby
        u_inLobby=serializedObject.FindProperty("u_inLobby");
        //Game Lobby Ui
        playerList=serializedObject.FindProperty("playerList");
        playerPanel=serializedObject.FindProperty("playerPanel");
        gameInfo=serializedObject.FindProperty("gameInfo");
    }
}
