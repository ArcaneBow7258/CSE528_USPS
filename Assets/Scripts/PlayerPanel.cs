using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerPanel : MonoBehaviour
{
    public TMP_Text nameO;
    public TMP_Text levelO;
    public TMP_Text readyO;
    public Button kick;
    public void initializePanel(string name, int level, string ready, bool host){
        nameO.text = name;
        levelO.text = "Level: " + level.ToString();
        readyO.text = ready;
        if(host){
            kick.onClick.AddListener(delegate {kickPlayer("placeholder");});
        }else{
            kick.gameObject.SetActive(false);
        }
        
    }
    private void kickPlayer(string playerId){

    }
    

}
