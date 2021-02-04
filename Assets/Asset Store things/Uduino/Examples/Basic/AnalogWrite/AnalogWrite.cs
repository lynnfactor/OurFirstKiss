using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;

public class AnalogWrite : MonoBehaviour {

    public int pin = 3;

    [Range(0, 255)]
    public int brightness;

	// Use this for initialization
	void Start () {
        UduinoManager.Instance.pinMode(pin, PinMode.Output);
	}
	
	// Update is called once per frame
	void Update () {
        UduinoManager.Instance.analogWrite(3, brightness);
    }

}
