using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwapCredits : MonoBehaviour
{
    // Start is called before the first frame update
    public Image creditsBackground;
    public Sprite[] sprites = new Sprite[9];
    
    IEnumerator Start()
    {
        yield return StartCoroutine("Wait", 2.0f);
    }

    

    private IEnumerator Wait(float wait)
    {
        int i = 0;
        while (i < 9)
        {
            yield return new WaitForSeconds(wait);
            creditsBackground.sprite = sprites[i];
            i++;
        }
    }
}
