using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAudienceStart : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField]
    private Animator anim;
    private bool enter = false;
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.SetBool("enter", false);
        //Debug.Log("Animation played");
        StartCoroutine(WaitAudience());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator WaitAudience(){
        yield return new WaitForSeconds(30f);
        anim.SetBool("enter", true);
        enter = true;
    }
}
