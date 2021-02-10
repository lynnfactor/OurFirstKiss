using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

/* script by Aubrey Isaacman
 * 
 * Based on Uduino ReadWrite example
 * This script hooks up all the Arduino inputs to the game
*/

public class ReadWrite : MonoBehaviour {

    UduinoManager u;

    // player 1 stuff
    int p1ReadValue = 0;
    int p1Left = 0;
    int p1Right = 0;

    // player 2 stuff
    int p2ReadValue = 0;
    int p2Left = 0;
    int p2Right = 0;

    void Start ()
    {
        u = UduinoManager.Instance;
        // player 1
        // pressure sensor
        u.pinMode(AnalogPin.A0, PinMode.Input);
        // LED
        u.pinMode(3, PinMode.Output);
        // push buttons
        u.pinMode(4, PinMode.Input_pullup);
        u.pinMode(2, PinMode.Input_pullup);

        // player 2
        // pressure sensor
        u.pinMode(AnalogPin.A3, PinMode.Input);
        // LED
        u.pinMode(6, PinMode.Output);
        // push buttons
        u.pinMode(5, PinMode.Input_pullup);
        u.pinMode(7, PinMode.Input_pullup);
    }

    void Update ()
    {
        ReadValue();
    }

    void ReadValue()
    {
        // when P1 pressure sensor is pushed,
        p1ReadValue = u.analogRead(AnalogPin.A0);
        // change their LED brightness to that relative value
        u.analogWrite(3, p1ReadValue/6);

        // when P2 pressure sensor is pushed
        p2ReadValue = u.analogRead(AnalogPin.A3);
        // change their LED brightness to that relative value
        u.analogWrite(6, p2ReadValue / 6);
    }
}
