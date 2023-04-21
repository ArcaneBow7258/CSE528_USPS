using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Text speedometer;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        speedometer.text = "Speed: " + speed.ToString();
    }
}
