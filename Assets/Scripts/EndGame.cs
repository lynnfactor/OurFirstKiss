using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Image creditsBackground;
    public GameObject creditsCanvas;
    public GameObject movieCanvas;
    public Sprite[] sprites = new Sprite[9];
    private ColorAdjustments color;
    
    public void End(){
        StartCoroutine(WaitAndDisplay(3.0f));
    }
    

    IEnumerator WaitAndDisplay(float wait)
    {
        movieCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        int i = 0;
        while (i < 9)
        {
            yield return new WaitForSeconds(wait);
            creditsBackground.sprite = sprites[i];
            i++;
        }
    }
}
