using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* created by Aubrey Isaacman
 *
 * This script will be used for the onboarding music
 * This script detects when an audio clip has ended and plays another one
 * 
 * Trying to add something so the start music continues to play when you switch from Start to Changing Room
 * and make sure there aren't duplicate music players when they restart the game
 *
 * following this Unity forum answer: https://answers.unity.com/questions/904981/how-to-play-an-audio-file-after-another-finishes.html
 * https://answers.unity.com/questions/1173303/how-to-check-which-scene-is-loaded-and-write-if-co.html
*/

public class PlayAudioAfter : MonoBehaviour
{

    public AudioClip playFirst;
    public AudioClip thenLoop;

    public string stopAtScene;

    public GameObject musicPlayer;

    private void Awake()
    {
        

        // check for existing object called "music"
        musicPlayer = GameObject.Find("MUSIC");


        // if it doesn't exist, then
        if (musicPlayer == null)
        {
            // set the object this script is attached to as the music player
            musicPlayer = this.gameObject;
            // rename THIS object to "MUSIC" for next time
            musicPlayer.name = "MUSIC";

            // tell this object NOT to die when changing scenes
            DontDestroyOnLoad(musicPlayer);
            //SceneManager.sceneLoaded += OnSceneLoaded();
 
        } else
        {
            if(this.gameObject.name != "MUSIC")
            {
                // if there is already an object in the scene called MUSIC, that means we've been here before
                // so we need to tell this object to destroy itself if it's not the original
                Destroy(this.gameObject);
            }
        } 
        
    }

    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // make temporary reference to the current scene
        Scene currentScene = SceneManager.GetActiveScene();

        // get name of this scene
        string sceneName = currentScene.name;

        // if the name of the current scene is the same as the scene you want this object to get destroyed in, kill it!
        if(sceneName == stopAtScene)
        {
            Destroy(this.gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().loop = true;
        StartCoroutine(playLoopSong());

    }

    void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
