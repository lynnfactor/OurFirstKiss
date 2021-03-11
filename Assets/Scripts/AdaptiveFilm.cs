﻿using System.Collections;
using System.Collections.Generic;
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

    // audio elements
    public AudioSource music;

    public AudioClip[] clips;
    //public AudioClip farSong;
    //public AudioClip closeSong;
    //public AudioClip kissSong;

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

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer.Pause();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextVideo = 0f;
        volume.profile.TryGet(out color);
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
            //music.clip = clips[1];
        } 
        // if they move far apart, stop this clip
        /*
        else
        {

        }
        */

        // if they're next to each other & kissing
        if(currentDist <= kissingDist && pm.isKissing) //KB: right now we're having trouble figuring out why isKissing isn't turning on
        {
            PlayFootage(KissingArray);
            color.saturation.value = 50f;
            //music.clip = clips[2];
        }
        // if they're not kissing anymore, stop this clip
        /*
        else
        {
            music.clip 
        }
        */
    }

    // this function takes in which array you want to call
    // then it plays the footage from that array
    void PlayFootage(VideoClip[] array)
    {
        if(Time.time > timeUntilNextVideo)
        {
            videoPlayer.clip = array[Random.Range(0, array.Length)];
            timeUntilNextVideo = Time.time + (float)videoPlayer.clip.length;
            videoPlayer.Play();
        }
    }
}
