using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LobbyInfo : MonoBehaviour
{
    public TMP_Text nameO;
    public TMP_Text codeO;
    public TMP_Text diffO;
    public TMP_Text lengthO;
    private string lobbyCode;
    public void updateLobbyInfo(string name, string code, string diff, string length){
        nameO.text = "Name: "+ name;
        codeO.text = "Code: "+code;
        diffO.text = "Difficulty: "+diff.ToString();
        lengthO.text = "Game Length: "+length.ToString();
    }


}
