using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* originally created by cloud
 * Aubrey is having trouble importing it so here's a new one
*/

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    private int[] emojiCounts;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        emojiCounts = new int[4];   // array size corresponds to how many emojis we have
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddClicks(int emojiNum)
    {
        emojiCounts[emojiNum]++;

        if(emojiCounts[emojiNum] >= 10)
        {
            audioSource.clip = clips[emojiNum];
            audioSource.Play();
            emojiCounts[emojiNum] = 0;
        }
    }
}
