using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/* created by Aubrey Isaacman
 *
 * This script controls the settings
 * Following this Brackeys tutorial: https://www.youtube.com/watch?v=YOaYQrN1oYQ&t=13s&ab_channel=Brackeys
 */

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    //public static float masterVolume;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
