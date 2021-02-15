using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAudienceStart : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    private Animation anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
}
