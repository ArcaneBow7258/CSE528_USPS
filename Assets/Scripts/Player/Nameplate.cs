using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Linq;
public class Nameplate : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;
    public void Awake()
    {
        text.text = "PogChampion";
    }
    public void update(){
        try{
            text.text = LobbyManager.Instance.currentLobby.Players.Where((p,t)=> {
                return NetworkManager.Singleton.LocalClientId.ToString().Equals((p.Data["ClientID"].Value));
            }).First().Data["Name"].Value;
            this.enabled = false;
        }catch{
            
        }
    }

}
