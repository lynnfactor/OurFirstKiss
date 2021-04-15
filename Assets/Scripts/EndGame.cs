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
    public GameObject adaptiveFilm;
    public Sound sound;
    private ColorAdjustments color;
    
    public void End(){
        adaptiveFilm.GetComponent<AdaptiveFilm>().enabled = false;
        movieCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        //color.saturation.value = 100f;
        //sound.source.Play();
        StartCoroutine(WaitAndDisplay(3.0f));
    }
    

    IEnumerator WaitAndDisplay(float wait)
    {
        int i = 0;
        while (i < 9)
        {
            Debug.Log(i);
            Debug.Log(sprites[i].name);
            yield return new WaitForSeconds(wait);
            creditsBackground.sprite = sprites[i];
            i++;
        }
    }
}
