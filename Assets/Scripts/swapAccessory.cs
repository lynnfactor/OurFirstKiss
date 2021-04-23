using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class swapAccessory : MonoBehaviour
{
    /*
    By Lex Yu (lsyu@usc.edu)
    Attached to the GameObjects floating above the seats. Each one has a SpriteRenderer that has the accessory sprite.

    */
    // Start is called before the first frame update
    public accessorySelect main;
    private SpriteRenderer rend; // this object's sprite renderer
    public GameObject P1dec;
    public GameObject P2dec;
    private Sprite[] all;
    private Sprite[] side;
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        all = main.All();
        side = main.Side();
        //all = Resources.LoadAll<Sprite>("accessories");
    }

    // swap the accessory when the player moves seats
    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log(gameObject.name);
        if (other.name == "P1"){
            P1dec.GetComponent<SpriteRenderer>().sprite = rend.sprite;
            
            PlayerPrefs.SetString("P1 accessory", rend.sprite.name);
            Debug.Log("P1: " + PlayerPrefs.GetString("P1 accessory"));
            int index = Array.FindIndex(all, s=> s.name == PlayerPrefs.GetString("P1 accessory"));
            Debug.Log( PlayerPrefs.GetString("P1 accessory"));
            Debug.Log(index);
            Debug.Log(all.Length);
            PlayerPrefs.SetString("P1 accessory side", side[index].name);
            
        }
        if (other.name == "P2"){
            P2dec.GetComponent<SpriteRenderer>().sprite = rend.sprite;
            
            PlayerPrefs.SetString("P2 accessory", rend.sprite.name);
            Debug.Log("P2: " + PlayerPrefs.GetString("P2 accessory"));
            int index = Array.FindIndex(all, s=> s.name == PlayerPrefs.GetString("P2 accessory"));
            //Debug.Log(all.length);
            PlayerPrefs.SetString("P2 accessory side", side[index].name);
            
        }
        main.SpawnNextAccessory(rend); // Load a new accessory
        
    }
}
