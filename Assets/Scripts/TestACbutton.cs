using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Uduino;

/*
 * created by Aubrey Isaacman
 * 
 * This script reads the digital input of a button from Arduino
 * 
 * Following this tutorial: https://www.youtube.com/watch?v=c8nQoixhiR4&list=PLt-5n3K_vUZeiJ1U5RBgEIBroh8imzu1E&index=3&ab_channel=MarcTeyssier
 */

public class TestACbutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UduinoManager.Instance.pinMode(2, PinMode.Input_pullup);
    }

    // Update is called once per frame
    void Update()
    {
        int buttonVal = UduinoManager.Instance.digitalRead(2);

        if(buttonVal == 0)
        {
            Debug.Log("button DOWN");
        }
        else
        {
            Debug.Log("button UP");
        }
        
    }
}
