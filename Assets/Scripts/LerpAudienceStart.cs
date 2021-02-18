using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAudienceStart : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    private Animator anim;
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("enter", true);
        Debug.Log("Animation played");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
