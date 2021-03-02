using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accessorySelect : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite[] all; // all the accessories
    public GameObject[] slots; // the 5 slots
    private SpriteRenderer[] slotrend;
    private int index = 7; // next available index
    void Start()
    {

    }

    // Update is called once per frame
    public void SpawnNextAccessory(SpriteRenderer rend){
        rend.sprite = all[index];
        index++;
        if (index >= all.Length){
            index = 0;
        }
    }
}
