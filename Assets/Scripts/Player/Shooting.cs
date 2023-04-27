using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Camera Cam;
    public bool isAutomatic;
    public float timeBetweenShot;
    public float gunDamage;
    public int currentMag;
    public int fullMag;
    public int currentReserve;
    public InventoryManager loadout;

    private int equippedWeapon = 1;

    private bool liftTrigger = true;
    private float lastShot = 0;

    void Start()
    {
        Cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { equippedWeapon = 1; }//weapon equips
        if (Input.GetKeyDown(KeyCode.Alpha2)) { equippedWeapon = 2; }
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentMag != 0)
        {
            if (Time.time > lastShot + timeBetweenShot)//firerate
            {
                if (isAutomatic)//automatic weapons
                {
                    Shoot();
                    currentMag--;
                }

                else if (liftTrigger)//single-fire weapons
                {
                    Shoot();
                    liftTrigger = false;
                    currentMag--;
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
            EnemyAI target = hitObject.GetComponent<EnemyAI>();
            if(target != null)
            {
                //target.React(gunDamage);
            }
        }
    }
}

