using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/*
 * created by Aubrey Isaacman
 *
 * plays the movie through the Canvas
*/


public class StreamMovie : MonoBehaviour
{
    // variables
    public RawImage rawImage;
    public VideoPlayer movie;
    public AudioSource sound;

    // initialize
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        movie.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        // if the movie is not prepared
        while(!movie.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }

        // assign raw image texture to movie texture
        rawImage.texture = movie.texture;
        movie.Play();
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
