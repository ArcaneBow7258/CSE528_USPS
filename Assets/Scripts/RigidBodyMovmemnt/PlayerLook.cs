using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float LookUpSpeed;
    public float TurnSpeed;
    public Camera Head;

    public float YTurn;
    public float XTurn;

    public float MaxLookAngle;
    public float MinLookAngle;


    void Start() { 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float CamX = Input.GetAxis("Mouse X");
        float CamY = Input.GetAxis("Mouse Y");

        LookUpDown(CamY);
        TurnPlayer(CamX, TurnSpeed);
    }

    void LookUpDown(float YAmt)
    {
        XTurn -= YAmt * LookUpSpeed;
        XTurn = Mathf.Clamp(XTurn, MinLookAngle, MaxLookAngle);

        Head.transform.localRotation = Quaternion.Euler(XTurn, 0, 0);
    }

    void TurnPlayer(float Xamt, float Spd)
    {
        YTurn += Xamt * Spd;

        transform.rotation = Quaternion.Euler(0, YTurn, 0);
    }
}
