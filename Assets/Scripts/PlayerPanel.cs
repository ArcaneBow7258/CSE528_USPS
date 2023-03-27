using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerPanel : MonoBehaviour
{
    public TMP_Text nameO;
    public TMP_Text levelO;
    public Button kick;
    public void initializePanel(string name, int level, bool host){
        nameO.text = name;
        levelO.text = level.ToString();
        if(host){
            kick.onClick.AddListener(delegate {kickPlayer("placeholder");});
        }else{
            kick.gameObject.SetActive(false);
        }
        
    }
    private void kickPlayer(string playerId){

    }
    

}
