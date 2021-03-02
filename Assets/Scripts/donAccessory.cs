using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class donAccessory : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject P1dec;
    public GameObject P2dec;
    private Sprite[] all;
    void Start()
    {
        Debug.Log(PlayerPrefs.GetString("P1 accessory"));
        Debug.Log(PlayerPrefs.GetString("P2 accessory"));
        
        all = Resources.LoadAll<Sprite>("accessories");
        int p1 = Array.FindIndex(all, s=> s.name == PlayerPrefs.GetString("P1 accessory"));
        P1dec.GetComponent<SpriteRenderer>().sprite = all[p1];
        Debug.Log("P1: " + P1dec.GetComponent<SpriteRenderer>().sprite.name);
        int p2 = Array.FindIndex(all, s=> s.name == PlayerPrefs.GetString("P2 accessory"));
        P1dec.GetComponent<SpriteRenderer>().sprite = all[p2];
        Debug.Log("P2: " + P2dec.GetComponent<SpriteRenderer>().sprite.name);
    }

}
