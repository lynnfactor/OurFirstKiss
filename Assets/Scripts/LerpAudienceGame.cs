using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpAudienceGame : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public GameObject P1;
    public GameObject P2;
    private int frames;
    bool enter = false;
    void Start()
    {
        frames = 0;
        animator = GetComponent<Animator>();
        animator.SetBool("enter", false);
    }

    // Update is called once per frame
    void Update()
    {
        while(Mathf.Abs(P1.transform.position.x - P2.transform.position.x) > 12f && !enter){
            frames++;
            if(frames > 60){
                frames = 0;
                enterAudience();
            }
        }
    }

    public void enterAudience(){
        enter = true;

        animator.SetBool("enter", true);
        StartCoroutine(WaitAudience());
    }

    IEnumerator WaitAudience(){
        yield return new WaitForSeconds(4f);
        animator.SetBool("isLeaving", true);
        animator.SetBool("enter", false);
    }
}
