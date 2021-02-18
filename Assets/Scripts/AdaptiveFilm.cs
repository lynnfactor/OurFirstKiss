using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/* created by Aubrey Isaacman
 * 
 * This script determines if the players are far apart (opposite seats), close (anything in the middle), or kissing
 * Depending on which one of those 3 states the players are in, a random video will play that corresponds to their distance
 * I imagine we will also play our adaptive audio through this script
 * *** this will work the same where depending on the distance and state of the players, we will add or take away an audio layer
 * 
 * Tutorials used:
 * https://forum.unity.com/threads/scroll-through-video-array-randomly.522563/
 */

public class AdaptiveFilm : MonoBehaviour
{
    // 3 different arrays for each distance
    public VideoClip[] FarArray;
    public VideoClip[] CloseArray;
    public VideoClip[] KissingArray;

    // video elements
    public VideoPlayer videoPlayer;
    private float timeUntilNextVideo;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        //videoPlayer.Pause();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextVideo = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeUntilNextVideo)
        {
            videoPlayer.clip = FarArray[Random.Range(0, FarArray.Length)];
            Debug.Log("shh the movie's starting");
            timeUntilNextVideo = Time.time + (float)videoPlayer.clip.length;
            videoPlayer.Play();
        }
    }
}
