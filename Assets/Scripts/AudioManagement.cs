using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagement : MonoBehaviour
{
    //comment here with emoji orders
    [SerializeField] AudioClip[] clips;
    private int[] emojiCounts;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        emojiCounts = new int[4]; //array size coresponds to how many emojis we have
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddClicks(int emojiNum)
    {
        emojiCounts[emojiNum]++;

        if (emojiCounts[emojiNum] >= 10 /*&& !audioSource.isPlaying*/)
                                        //if we want to have only one SFX played at a time, enable this condition
        {
            audioSource.clip = clips[emojiNum];
            audioSource.Play();
            emojiCounts[emojiNum] = 0;
        }
    }

    /*IEnumerator ClearCount()
    {
        while(isAnyClipPlaying)
        {
            if (!audioSource.isPlaying)
            {
                isAnyClipPlaying = false;
                clickCount = 0;
            }
            yield return null;
        }
    }*/

}
