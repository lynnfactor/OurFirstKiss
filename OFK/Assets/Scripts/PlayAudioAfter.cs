using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* created by Aubrey Isaacman
 *
 * This script will be used for the onboarding music
 * This script detects when an audio clip has ended and plays another one
 *
 * following this Unity forum answer: https://answers.unity.com/questions/904981/how-to-play-an-audio-file-after-another-finishes.html
*/

public class PlayAudioAfter : MonoBehaviour
{

    public AudioClip playFirst;
    public AudioClip thenLoop;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().loop = true;
        StartCoroutine(playLoopSong());
    }

    IEnumerator playLoopSong()
    {
        GetComponent<AudioSource>().clip = playFirst;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        GetComponent<AudioSource>().clip = thenLoop;
        GetComponent<AudioSource>().Play();
    }

}
