using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class accessorySelect : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] all; // all the accessories
    public GameObject[] slots; // the 5 slots
    private SpriteRenderer[] slotrend;
    private int index = 0; // next available index
    void Start()
    {
        all = Resources.LoadAll<Sprite>("accessories");
    }

    // get the next available accessory and spawn it, then return the index of the accessory the player is now wearing
    public void SpawnNextAccessory(SpriteRenderer rend){
        
        index++;
        if (index >= all.Length){
            index = 0;
        }
        rend.sprite = all[index];
    }
}
