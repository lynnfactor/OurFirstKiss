using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class swapAccessory : MonoBehaviour
{
    // Start is called before the first frame update
    public accessorySelect main;
    private SpriteRenderer rend; // this object's sprite renderer
    public GameObject P1dec;
    public GameObject P2dec;
    //private Sprite[] all;
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        PlayerPrefs.SetString("P1 accessory", "decorations-04_1");
        PlayerPrefs.SetString("P2 accessory", "decorations-04_2");
        //all = Resources.LoadAll<Sprite>("accessories");
    }

    // swap the accessory when the player moves seats
    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(gameObject.name);
        if (other.name == "P1"){
            P1dec.GetComponent<SpriteRenderer>().sprite = rend.sprite;
            Debug.Log("P1: " + rend.sprite.name);
            PlayerPrefs.SetString("P1 accessory", rend.sprite.name);
            
        }
        if (other.name == "P2"){
            P2dec.GetComponent<SpriteRenderer>().sprite = rend.sprite;
            Debug.Log("P2: " + rend.sprite.name);
            PlayerPrefs.SetString("P2 accessory", rend.sprite.name);
            
        }
        main.SpawnNextAccessory(rend);
        
    }
}
