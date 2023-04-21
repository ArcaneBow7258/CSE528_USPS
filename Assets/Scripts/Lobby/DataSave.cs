using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public static class SaveSystem 
{
    public static void SaveTree(SaveFile tree){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, tree);
        stream.Close();

    }
    public static SaveFile LoadTree(){
         //\AppData\LocalLow\DefaultCompany\CSE528_USPS
         string path = Application.persistentDataPath + "/player.txt";
         
         if(File.Exists(path)){
            Debug.Log(path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            stream.Position = 0;
            SaveFile ret = (SaveFile)formatter.Deserialize(stream) ; 
            stream.Close();
            return ret;

         }else{
            Debug.Log("No Save file");
            return null;
         }
    }
}

[System.Serializable]
public class SaveFile{
    public int pp;
    public List<int> playerTalents = new List<int>();
    public List<int> equippedAbilities= new List<int>();
    public SaveFile(SkillTree tree){
        pp = tree.pp + tree.playerTalents.Count;
        foreach(talent k in tree.playerTalents){
            playerTalents.Add(k.id);
        }
        foreach(talent k in tree.equippedAbilities){
            equippedAbilities.Add(k.id);
        }
    }
}
