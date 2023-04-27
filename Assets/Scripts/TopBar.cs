using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
public class TopBar : NetworkBehaviour
{
    public bool timer = false;
    public float cooldown = 0;
    private float lastHit = 0;
    [Header("Game Objects")]
    public GameObject bars;
    public Image lifebar;
    public Image shieldbar;
    public GeneralStats reference;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(!timer) return;
        bars.SetActive(false);
        reference.life.OnValueChanged += OnLifeChanged;
        reference.shield.OnValueChanged += OnLifeChanged;
    }
    public void OnLifeChanged(float prev, float cur){
        if(cur <= prev){
                lastHit = Time.time;
                bars.SetActive(true);
        }else{
            //lastHit = Time.time;
        }
    }
    void Update(){
        lifebar.fillAmount =  reference.life.Value / reference.maxLife.Value;
        //Debug.Log(lastHit.ToString() + " + " + cooldown.ToString() + " < " +Time.time.ToString());
        if(reference.maxShield.Value > 0){
            shieldbar.fillAmount = reference.shield.Value / reference.maxShield.Value;
            shieldbar.gameObject.SetActive(true);
        }else{
            shieldbar.gameObject.SetActive(false);
        }
        if(!timer) return;
        if(lastHit +cooldown < Time.time){
            bars.SetActive(false);
        }else{
            bars.SetActive(true);
        }
        
    }
}
