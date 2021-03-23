using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/* created by Aubrey Isaacman and Lex Yu
 * 
 * This script determines if the players are far apart (opposite seats), close (anything in the middle), or kissing
 * Depending on which one of those 3 states the players are in, a random video will play that corresponds to their distance
 * I imagine we will also play our adaptive audio through this script
 * *** this will work the same where depending on the distance and state of the players, we will add or take away an audio layer
 * 
 * It also adjusts the saturation of the videos based on how far the players are.
 *
 * Tutorials used:
 * https://forum.unity.com/threads/scroll-through-video-array-randomly.522563/
 * https://www.codegrepper.com/code-examples/csharp/unity+how+to+play+video+on+canvas
 * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/passing-arrays-as-arguments
 * https://answers.unity.com/questions/1418348/how-do-i-access-a-bool-from-another-script.html
 */

//Audio Stuff KB
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    public bool loop;

    [HideInInspector]
    public AudioSource source;

}

public class AdaptiveFilm : MonoBehaviour
{
    // reference to PlayerMovement script
    public PlayerMovement pm;

    // 3 different arrays for each distance
    public VideoClip[] FarArray;
    public VideoClip[] CloseArray;
    public VideoClip[] KissingArray;

    // video elements
    public VideoPlayer videoPlayer;
    private float timeUntilNextVideo;

    //KB  note: I'm temporarily hiding this while I experiment with my own AudioManager!
    //public AudioSource music;

    //public AudioClip[] clips;
    //public AudioClip farSong;
    //public AudioClip closeSong;
    //public AudioClip kissSong;

    //Audio Stuff KB
    [SerializeField] Sound[] sounds;
    private bool fadeinClose, fadeoutClose, fadeinKiss, fadeoutKiss;

    // for player distances
    public GameObject p1;
    public GameObject p2;

    Transform player1;
    Transform player2;
    
    //detect distance
    public float closeDist;
    public float kissingDist;
    public float currentDist;

    // saturation adjustment
    public Volume volume;
    private ColorAdjustments color;

    //Audio Stuff KB
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer.Pause();

        //Audio Stuff KB
        //loop gives properties to ALL music files in the array
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextVideo = 0f;
        volume.profile.TryGet(out color);

        //Audio Stuff KB
        //play all 3 music files on Start
        Play("Far");
        Play("Close");
        Play("Kiss");

        //set volumes: want the far apart to be 1, while everything else is 0
        sounds[0].source.volume = 1f;
        sounds[1].source.volume = 0f;
        sounds[2].source.volume = 0f;

        //loop all music files
        sounds[0].source.loop = true;
        sounds[1].source.loop = true;
        sounds[2].source.loop = true;

        //these are like 'locks' for the fadein/fadeout coroutines... idk... cloud knows this lol
        fadeinClose = false;
        fadeoutClose = false;
        fadeinKiss = false;
        fadeoutKiss = false;

    }

    // Update is called once per frame
    void Update()
    {
        //music.Play();
        // get transform for players
        if(p1 != null)
        {
            player1 = p1.transform;
        }
        if (p2 != null)
        {
            player2 = p2.transform;
        }

        currentDist = Vector2.Distance(player1.position, player2.position);

        // if players are far apart
        if(currentDist > closeDist)
        {
            PlayFootage(FarArray);
            color.saturation.value = -80f;
        }

        //if players are closer than the farthest distance
        if(currentDist <= closeDist)
        {
            PlayFootage(CloseArray);
            color.saturation.value = 0f;

            //Audio Stuff KB - fade in "close" music
            StartCoroutine(FadeIn(sounds[1].source));
            fadeinClose = true;

        } 
        // Audio Stuff KB - if they move far apart, fade out "close" music
        else
        {
            StartCoroutine(FadeOut(sounds[1].source));
            fadeoutClose = true;
        }

        // if they're next to each other & kissing
        if(currentDist <= kissingDist && pm.isKissing) //KB: right now we're having trouble figuring out why isKissing isn't turning on
        {
            PlayFootage(KissingArray);
            color.saturation.value = 50f;

            //Audio Stuff KB - fade in "KISSING" music
            StartCoroutine(FadeIn(sounds[2].source));
            fadeinKiss = true;

        }
        // Audio Stuff KB - if they're not kissing anymore, stop "KISSING" music
        else
        {
            StartCoroutine(FadeOut(sounds[2].source));
            fadeoutKiss = true;
        }
    }

    // this function takes in which array you want to call
    // then it plays the footage from that array
    void PlayFootage(VideoClip[] array)
    {
        if(Time.time > timeUntilNextVideo)
        {
            videoPlayer.clip = array[UnityEngine.Random.Range(0, array.Length)];
            timeUntilNextVideo = Time.time + (float)videoPlayer.clip.length;
            videoPlayer.Play();
        }
    }

    //Audio Stuff KB - these are coroutines that cloud helped me make to fadein and fadeout audio :D
    //need to examine why the fadeins and fadeouts aren't happening... 3/23/21
    IEnumerator FadeIn(AudioSource track)
    {
        while (track.volume < 1)
        {
            track.volume += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        fadeinClose = false;
        fadeinKiss = false;
    }

    IEnumerator FadeOut(AudioSource track)
    {
        while (track.volume > 0)
        {
            track.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        fadeoutClose = false;
        fadeoutKiss = false;
    }
}
