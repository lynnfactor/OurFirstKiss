﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class accessorySelect : MonoBehaviour
{
    /*
    By Lex Yu (lsyu@usc.edu)
    This script loads all the accessories from the Resources folder (do NOT remove the acessories from the Resources folder!!)

    */
    // Start is called before the first frame update
    public Sprite[] all; // all the accessories
    public Sprite[] side; // all the side views of the accessories
    public GameObject[] slots; // the 5 slots
    private SpriteRenderer[] slotrend;
    private int index = 7; // next available index
    void Start()
    {
        //all = Resources.LoadAll<Sprite>("accessories");
        PlayerPrefs.SetString("P1 accessory", "decorations1_0");
        PlayerPrefs.SetString("P1 accessory side", "decorations1_2");
        PlayerPrefs.SetString("P2 accessory", "decorations3_0");
        PlayerPrefs.SetString("P1 accessory side", "decorations3_1");
    }

    // get the next available accessory and spawn it, then return the index of the accessory the player is now wearing
    // this function is called by the GameObjects floating above the seats
    public void SpawnNextAccessory(SpriteRenderer rend){
        
        index++;
        if (index >= all.Length){
            index = 0;
        }
        rend.sprite = all[index];
        
    }
    public Sprite[] All(){
        return all;
    }
    public Sprite[] Side(){
        return side;
    }
}
