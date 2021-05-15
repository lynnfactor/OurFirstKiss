using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAudienceGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public GameObject P1;
    public GameObject P2;
    private int frames;
    private float period;
    private float nextActionTime;
    bool enter = false;
    void Start()
    {
        frames = 0;
        nextActionTime = 5.0f;
        period = 20.0f;
        animator.SetBool("enter", false);
        //Debug.Log("Set enter to false");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            enterAudience();
        }
    }


    
    public void enterAudience(){
        if(!enter && Mathf.Abs(P1.transform.position.x - P2.transform.position.x) > 12f){
            animator.SetBool("exit", false);
            animator.SetBool("enter", true);
            //Debug.Log("set enter to true");
            StartCoroutine(WaitAudience());
        }
    }


    IEnumerator WaitAudience(){
        yield return new WaitForSeconds(5f);
        animator.SetBool("exit", true);
        animator.SetBool("enter", false);
        enter = false;
        //Debug.Log("waiting");
    }
    
}
