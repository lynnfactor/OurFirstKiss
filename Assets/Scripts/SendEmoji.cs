using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using NDream.AirConsole;
//using Newtonsoft.Json.Linq;

public class SendEmoji : MonoBehaviour
{
    public AudioManager amScript;
    [SerializeField] GameObject[] emojiPrefab;
    [SerializeField] Canvas canvas;
    [SerializeField] float randomRangeMax = 200f;
    //[SerializeField] int emojiNum;
    
    private RectTransform canvasRTrans;
    private Vector3 startPos;

    private void Awake()
    {
        //AirConsole.instance.onMessage += OnMessage;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasRTrans = canvas.GetComponent<RectTransform>();
        startPos = new Vector3(Random.Range(0f, randomRangeMax), 0f, 0f);
    }
    
    public void InitEmoji(int emojiNum)
    {
        startPos.x = Random.Range(0f, randomRangeMax);
        Instantiate(emojiPrefab[emojiNum], startPos, Quaternion.identity, canvasRTrans);
        AddClickCount(emojiNum);
    }

    public void AddClickCount(int emojiNum)
    {
        amScript.AddClicks(emojiNum);
    }

    /*void OnMessage(int fromDeviceID, JToken data)
    {
        Debug.Log("message from" + fromDeviceID + ", data:" + data);
        if (data["action"] != null && data["action"].ToString().Equals("interact"))
        {
            SendEmoji();
        }
    }*/

    /*private void OnDestroy()
    {
        //unregistered events
        if (AirConsole.instance != null)
        {
            AirConsole.instance.onMessage -= OnMessage;
        }
    }*/
}
