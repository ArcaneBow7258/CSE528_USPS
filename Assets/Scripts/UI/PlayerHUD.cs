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
    public GameObject a1;
    private talent a1Stat;
    public GameObject a2;
    private talent a2Stat;
    [Header("Bars")]
    public Image lifeBar;
    public Image shieldBar;
    public TMP_Text healthText;
    [Header("Other")]
    public TMP_Text pointText;
    public void Awake(){
        switch(playerStats.tree.equippedAbilities.Count){
            case 0:
                Destroy(a1);
                Destroy(a2);
                break;
            case 1:
                Destroy(a2);
                break;
            default:
                break;
           
        }
        if(a1 != null && playerStats.tree.equippedAbilities.Count > 0){
            a1Stat = playerStats.tree.equippedAbilities[0];
            a1.transform.GetChild(0).GetComponent<Image>().sprite = a1Stat.icon;
        }
        if(a2 != null && playerStats.tree.equippedAbilities.Count > 1){
            a2Stat = playerStats.tree.equippedAbilities[1];
            a2.transform.GetChild(1).GetComponent<Image>().sprite = a2Stat.icon;
        }
    }
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

        //abilities
        if(a1Stat != null){
            a1.transform.GetChild(1).GetComponent<Image>().fillAmount = a1Stat.ability.cooldown / a1Stat.ability.cooldownMax;
        }   
        if(a2Stat != null){
            a2.transform.GetChild(1).GetComponent<Image>().fillAmount = a2Stat.ability.cooldown / a2Stat.ability.cooldownMax;
        }

    }
}
