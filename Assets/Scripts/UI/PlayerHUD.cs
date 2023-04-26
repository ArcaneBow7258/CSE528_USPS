using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHUD : MonoBehaviour
{
    public PlayerStats playerStats;
    
    [Header("Ammo")]
    public Image weapon;
    public TMP_Text ammoText;
    [Header("Bars")]
    public Image lifeBar;
    public Image shieldBar;
    public TMP_Text healthText;
    [Header("Other")]
    public TMP_Text pointText;
    public void FixedUpdate(){
        string holder = "[ ";
        
        if(playerStats.maxShield.Value > 0){
            shieldBar.fillAmount = playerStats.shield.Value / playerStats.maxShield.Value;
            shieldBar.gameObject.SetActive(true);
            holder += "<color=blue>" +  playerStats.shield.Value.ToString() +"/"+ playerStats.maxShield.Value.ToString() + "</color> | ";
        }else{
            shieldBar.gameObject.SetActive(false);
        }

        lifeBar.fillAmount =  playerStats.life.Value / playerStats.maxLife.Value;
        holder += "<color=green>" + playerStats.life.Value.ToString() +"/"+ playerStats.maxLife.Value.ToString() + "</color>";
        healthText.text = holder + " ]";
        pointText.text = "Points: " + GameManager.Instance.points.Value.ToString();
    }
}
