using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

/* created by Aubrey Isaacman
 *
 * This script changes the scene when the movie is over
 * We'll use this to change to the credits scene
 *
 * Following this code on Unity Answers: https://answers.unity.com/questions/1479729/load-scene-after-video-ended.html
*/

public class EndOfMovie : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // drag and drop GO holding videoplayer component here
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;    
    }

    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneName);
    }
}
