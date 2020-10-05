using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using NDream.AirConsole;
//using Newtonsoft.Json.Linq;

public class SendEmojiButton : MonoBehaviour
{
    [SerializeField] GameObject emojiPrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject audioManager;
    [SerializeField] float randomRangeMax = 200f;
    [SerializeField] int emojiNum;
    private RectTransform canvasRTrans;
    private AudioManagement amScript;
    private Vector3 startPos;
    
/*
    private void Awake()
    {
        AirConsole.instance.onMessage += OnMessage;
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        canvasRTrans = canvas.GetComponent<RectTransform>();
        amScript = audioManager.GetComponent<AudioManagement>();
        startPos = new Vector3(Random.Range(0f, randomRangeMax), 0f, 0f);
    }
    
    public void SendEmoji()
    {
        Instantiate(emojiPrefab, startPos, Quaternion.identity, canvasRTrans);
        startPos.x = Random.Range(0f, randomRangeMax);
    }

    public void AddClickCount()
    {
        //note: emojiNum starts from 1, not 0!
        amScript.AddClicks(emojiNum);
    }

/*
    void OnMessage(int fromDeviceID, JToken data)
    {
        Debug.Log("message from" + fromDeviceID + ", data:" + data);
        if (data["action"] != null && data["action"].ToString().Equals("interact"))
        {
            SendEmoji();
        }
    }
*/

/*
    private void OnDestroy()
    {
        //unregistered events
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }
*/
}
