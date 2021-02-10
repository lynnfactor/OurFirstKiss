using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class ReadWrite : MonoBehaviour {

    UduinoManager u;
    int p1ReadValue = 0;
    int p2ReadValue = 0;

    void Start ()
    {
        u = UduinoManager.Instance;
        // player 1
        u.pinMode(AnalogPin.A0, PinMode.Input);
        u.pinMode(3, PinMode.Output);

        // player 2
        u.pinMode(AnalogPin.A3, PinMode.Input);
        u.pinMode(6, PinMode.Output);
    }

    void Update ()
    {
        ReadValue();
    }

    void ReadValue()
    {
        p1ReadValue = u.analogRead(AnalogPin.A0);
        u.analogWrite(3, p1ReadValue/6);

        p2ReadValue = u.analogRead(AnalogPin.A3);
        u.analogWrite(6, p2ReadValue / 6);
    }
}
