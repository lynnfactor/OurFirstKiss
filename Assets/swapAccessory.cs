using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapAccessory : MonoBehaviour
{
    // Start is called before the first frame update
    public accessorySelect main;
    private SpriteRenderer rend; // this object's sprite renderer
    public GameObject P1dec;
    public GameObject P2dec;
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){
        if (other.name == "P1"){
            P1dec.GetComponent<SpriteRenderer>().sprite = rend.sprite;
        }
        else if (other.name == "P2"){
            P2dec.GetComponent<SpriteRenderer>().sprite = rend.sprite;
        }
        main.SpawnNextAccessory(rend);
    }
}
