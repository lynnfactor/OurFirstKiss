using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class donAccessory : MonoBehaviour
{
    /*
    By Lex Yu (lsyu@usc.edu)
    Sets the correct accessories based on what the players chose in the start screen.
    */
    // Start is called before the first frame update
    public GameObject P1dec;
    public GameObject P2dec;
    private Sprite[] accessories;
    void Start()
    {
        Debug.Log("P1 playerpref: " + PlayerPrefs.GetString("P1 accessory"));
        Debug.Log("P2 playerpref: " + PlayerPrefs.GetString("P2 accessory"));
        
        accessories = Resources.LoadAll<Sprite>("Sprites/accessories");
        int p1 = Array.FindIndex(accessories, s=> s.name == PlayerPrefs.GetString("P1 accessory"));
        P1dec.GetComponent<SpriteRenderer>().sprite = accessories[p1];
        Debug.Log("P1 game: " + P1dec.GetComponent<SpriteRenderer>().sprite.name);
        int p2 = Array.FindIndex(accessories, s=> s.name == PlayerPrefs.GetString("P2 accessory"));
        P2dec.GetComponent<SpriteRenderer>().sprite = accessories[p2];
        Debug.Log("P2 game: " + P2dec.GetComponent<SpriteRenderer>().sprite.name);
    }

}
