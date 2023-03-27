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
    public Button join;
    private string lobbyCode;
    public void updateLobbyInfo(string name, string code, string diff, string length){
        nameO.text = name;
        codeO.text = 
        diffO.text = diff.ToString();
        lengthO.text = length.ToString();
    }


}
