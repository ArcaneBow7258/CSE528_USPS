using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Shooting : NetworkBehaviour
{
    public InventoryManager manager;
    public Camera Cam;
    [HideInInspector]
    public Weapon ew;
    public LineRenderer lr;
    private bool liftTrigger = true;
    private float lastShot = 0;
    private Coroutine routine;
    [HideInInspector]
    public bool reloading;
    private Coroutine reloadRoutine;
    private float reloadStart;
    public override void OnNetworkSpawn(){
        if(IsOwner){
            ew = manager.weapons[0];
        }
    }
    void Start()
    {

        
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) { ew =  manager.weapons[0]; if(reloadRoutine != null) {StopCoroutine(reloadRoutine);} reloading = false; }//weapon equips
        if (Input.GetKeyDown(KeyCode.Alpha2)) {  ew =  manager.weapons[1]; if(reloadRoutine != null)  {StopCoroutine(reloadRoutine);} reloading = false; }
        if (Input.GetKeyDown(KeyCode.R) && !reloading)//reload gun
        {   
            reloadRoutine = StartCoroutine(ReloadRoutine(ew.reloadTime));
            
        }
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0))  && ew.currentMag != 0)
        {
            if(reloadRoutine != null) {StopCoroutine(reloadRoutine);} reloading = false;
            if (Time.time > lastShot + ew.timePerShot)//firerate
            {
                if (ew.based.isAutomatic)//automatic weapons
                {
                    Shoot();
                    ew.currentMag--;
                }

                else if (liftTrigger)//single-fire weapons
                {
                    Shoot();
                    liftTrigger = false;
                    ew.currentMag--;
                }
                lastShot = Time.time;
            }
        }else if(ew.currentMag <= 0){
            if(!reloading) reloadRoutine = StartCoroutine(ReloadRoutine(ew.reloadTime));
            
        }
        else
        {
            liftTrigger = true;
        }
    }
    private void Shoot()
    {
        Ray ray = Cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            LineRendererServerRpc(Cam.transform.position,hit.point, ew.based.rayColor);
            GameObject hitObject = hit.transform.gameObject;
            //Debug.Log(hit.point);
            try{
                EnemyStat target = hitObject.GetComponent<EnemyStat>();
                if(target != null)
                {
                    target.DealDamageServerRpc(ew.weaponDamage);
                }
            }catch{}//miss
            
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void LineRendererServerRpc(Vector3 camPos, Vector3 hit, Color rayColor){
        SetLineRenderClientRpc(camPos, hit,rayColor);
    }

    [ClientRpc()]
    public void SetLineRenderClientRpc(Vector3 camPos, Vector3 hit,  Color rayColor,ClientRpcParams clientRpcParams = default){
        //Debug.Log("render");
        lr.startColor = rayColor;
        lr.endColor = rayColor;
        lr.SetPosition(0,camPos);
        lr.SetPosition(1,hit);
        lr.enabled = true;
        if(routine != null){ StopCoroutine(routine);}
        routine = StartCoroutine(ClearLines());
    }
    public IEnumerator ClearLines(){
        yield return new WaitForSeconds(0.5f);
        lr.enabled = false;
    }
    public IEnumerator ReloadRoutine(float reloadTime){
        reloading = true;
        reloadStart = Time.time;
        yield return new WaitForSeconds(reloadTime);
        if (ew.ammoReserve >= ew.magSize - ew.currentMag )
            {
                ew.ammoReserve  -= ew.magSize - ew.currentMag ;
                ew.currentMag = ew.magSize;
            }
        else if (ew.ammoReserve > 0)
        {
            ew.currentMag = ew.ammoReserve;
            ew.ammoReserve = 0;
        }
        reloading = false;
        yield break;
    }
    public float GetReloadProgress(){
        return (Time.time - reloadStart) / ew.reloadTime;
    }
}

