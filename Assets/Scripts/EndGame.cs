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
    public Volume vol;
    private ColorAdjustments color;
    private AudioSource[] allAudioSources;
 
    
    public void End(){

        adaptiveFilm.GetComponent<AdaptiveFilm>().enabled = false;
        movieCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        vol.profile.TryGet(out color);
        color.saturation.value = 100f;
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach(AudioSource audioS in allAudioSources)  {
            audioS.Stop();
        }
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.Play();
        StartCoroutine(WaitAndDisplay(4.0f));
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
