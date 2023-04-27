using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Camera Cam;
    public InventoryManager loadout;

    public bool isAutomatic;
    public float timeBetweenShot;
    public float gunDamage;
    public int currentMag;
    public int fullMag;
    public int currentReserve;
    public Weapon[] weaponSlots;

    private int equippedWeapon = 0;

    private bool liftTrigger = true;
    private float lastShot = 0;

    void Start()
    {
        Cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { equippedWeapon = 0; }//weapon equips
        if (Input.GetKeyDown(KeyCode.Alpha2)) { equippedWeapon = 1; }
        if (Input.GetKeyDown(KeyCode.R))//reload gun
        {
            if (currentReserve >= fullMag)
            {
                currentReserve = currentReserve - fullMag + currentMag;
                currentMag = fullMag;
            }
            else if (currentReserve > 0)
            {
                currentMag = currentReserve;
                currentReserve = 0;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) && currentMag != 0)
        {
            if (Time.time > lastShot + timeBetweenShot)//firerate
            {
                if (isAutomatic)//automatic weapons
                {
                    Shoot();
                    currentMag--;
                    lastShot = Time.time;
                }

                else if (liftTrigger)//single-fire weapons
                {
                    Shoot();
                    liftTrigger = false;
                    currentMag--;
                    lastShot = Time.time;
                }
            }
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
            GameObject hitObject = hit.transform.gameObject;
            EnemyStat target = hitObject.GetComponent<EnemyStat>();
            if(target != null)
            {
                target.DealDamageServerRpc(gunDamage);
            }
        }
    }
}

