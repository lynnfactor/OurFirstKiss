using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmojiMovement : MonoBehaviour
{
    [SerializeField] float vSpeed = 5f; //vertical speed
    [SerializeField] float hSpeed = 5f; //horizontal speed
    [SerializeField] float canvasHeight = 590f;
    [SerializeField] float fadeRate = 0.005f;
    private RectTransform emojiRT;
    private float hTimer = 0f; //an accumulative timer for horizontal movement
    private Vector2 crtPos, deltaPos; //crtPos: current position
    
    private SpriteRenderer emoImg; //emoji image
    private Color emoColor; //emoji color

    void Start()
    {
        //initial setup to record the current location of the emoji
        emojiRT = this.GetComponent<RectTransform>();
        crtPos = emojiRT.anchoredPosition;
        emoImg = this.GetComponent<SpriteRenderer>();
        emoColor = emoImg.color;
    }

    void Update()
    {
        UpdateEmojiPosition();
        if (emojiRT.anchoredPosition.y >= canvasHeight / 2)
            StartCoroutine("EmojiFade");
        CheckBoundary();
    }

    /*
     * CheckBoundary() checks the current position against the max height of frame.
     * If the emoji is out of the canvas frame, it will destroy itself.
     */
    void CheckBoundary()
    {
        if (crtPos.y >= canvasHeight)
        {
            Destroy(this.gameObject, 0f);
        }
    }

    //calculate the delta movement distant and update emoji position
    void UpdateEmojiPosition()
    {
        hTimer += Time.deltaTime;
        deltaPos = new Vector2(hSpeed * Mathf.Sin(10 * hTimer), vSpeed * Time.deltaTime);
        crtPos += deltaPos;
        emojiRT.anchoredPosition = crtPos;
    }

    /*
     * EmojiFade() linearly fades out the emoji over time
     */
    IEnumerator EmojiFade()
    {
        for (float alpha = 1.0f; alpha >= 0; alpha -= fadeRate)
        {
            emoColor.a = alpha;
            emoImg.color = emoColor;
            yield return null;
        }
    }
}
