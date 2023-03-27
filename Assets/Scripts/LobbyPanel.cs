using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LobbyPanel : MonoBehaviour
{
    public TMP_Text nameO;
    public TMP_Text sizeO;
    public TMP_Text diffO;
    public TMP_Text lengthO;
    public Button join;
    public void initializePanel(string name, int numP, int maxP, string diff, string length, string id){
        nameO.text = name;
        sizeO.text = numP.ToString() + "/" + maxP.ToString();
        diffO.text = diff.ToString();
        lengthO.text = length.ToString();

        join.onClick.AddListener(delegate {joinLobby(id);});
    }
    private void joinLobby(string lobbyId){
        LobbyManager.Instance.joinById(lobbyId);
    }



}
