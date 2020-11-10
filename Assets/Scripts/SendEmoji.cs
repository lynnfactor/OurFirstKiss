using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/* originally created by Cloud
 * and yet another script where Aubs gets really lame errors so here I am
 * let's do iiiiiiitttt
*/

public class SendEmoji : MonoBehaviour
{
    [SerializeField] GameObject[] emojiPrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject audioManager;
    [SerializeField] float randomRangeMax = 200f;

    private RectTransform canvasRTrans;
    private AudioManager amScript;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        canvasRTrans = canvas.GetComponent<RectTransform>();
        amScript = audioManager.GetComponent<AudioManager>();
        startPos = new Vector3(Random.Range(0f, randomRangeMax), 0f, 0f);
    }

    public void InitEmoji(int emojiNum)
    {
        Instantiate(emojiPrefab[emojiNum], startPos, Quaternion.identity, canvasRTrans);
        startPos.x = Random.Range(0f, randomRangeMax);
        AddClickCount(emojiNum);
    }

    public void AddClickCount(int emojiNum)
    {
        amScript.AddClicks(emojiNum);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
