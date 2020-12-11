using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* created by Aubrey Isaacman
 *
 * This script plays the still images for the instructions in a random order for a certain amount of time
 * Following this Unity Answers for slideshow: https://answers.unity.com/questions/1327404/how-do-you-script-to-show-random-image-for-seconds.html
 * Following these Unity Answers for fading the images in/out:
        https://answers.unity.com/questions/654836/unity2d-sprite-fade-in-and-out.html
        https://answers.unity.com/questions/1625587/help-with-fading-selected-canvas-objects.html        
 */

public class SlideShowAds : MonoBehaviour
{
    // how much time you want the images to stay on screen
    public float time_min = 1f;
    public float time_max = 3f;
    // array to hold each image
    public Sprite[] sprites = new Sprite[0];
    public Image image;
    // to mess with transparency
    //public SpriteRenderer spRend;
    public CanvasGroup canvas;

    // to keep track of which sprite was most recently called
    public int prevSprite;
    public int currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowRandomImage");
        prevSprite = 0;
    }

    // cycle through the images
    public IEnumerator ShowRandomImage()
    {
        while (true)
        {
            // fade in
            StartCoroutine(FadeIn(0.0f, 1.0f));

            // assign random sprite to current sprite variable
            currentSprite = Random.Range(0, sprites.Length);
            image.sprite = sprites[currentSprite];

            // if the current sprite is the same as the previous sprite, call this function again
            if(currentSprite == prevSprite)
            {
                ShowRandomImage();
            } else
            {
                image.enabled = true;

                // this image becomes the previous image
                prevSprite = currentSprite;
            }
            

            yield return new WaitForSeconds(Random.Range(time_min, time_max));

            // fade out
            StartCoroutine(FadeOut(1.0f, 0.0f));

        }
    }

    public IEnumerator FadeIn(float aValue, float bValue)
    {
        for (float t = aValue; t < bValue; t += Time.deltaTime)
        {
            canvas.alpha = t;
            yield return null;
        }
    }

    // fade out
    public IEnumerator FadeOut(float aValue, float bValue)
    {
        for (float t = aValue; t > bValue; t -= Time.deltaTime)
        {
            canvas.alpha = t;
            yield return null;
        }
    }
}
