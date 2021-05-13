using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System.Collections;

public class TwitchChatClient : MonoBehaviour
{
    [Header("config.json file with 'username', 'userToken' and 'channelName'")]
    [SerializeField] private string configurationPath = "";
    [Header("Command prefix, by default is '!' (only 1 character)")]
    [SerializeField] private string commandPrefix = "!";
    [Header("Automatic initialize, otherwise it is necessary to call 'Init'")]
    [SerializeField] private bool automaticInit = true;

    //[SerializeField] GameObject audioManager;
    [SerializeField] SendEmoji sendEmojiScript;
    
    //note: define all the emoji indexes here as constants. Use them to call initEmoji function
    const int Heart = 0;
    const int Boo = 1;
    const int Laugh = 2;
    const int Gossip = 3;
    const int Aww = 4;
    const int Smooch = 5;
    const int WooHoo = 6;


    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    private TwitchConnectData data;

    public delegate void OnChatMessageReceived(TwitchChatMessage chatMessage);
    public OnChatMessageReceived onChatMessageReceived;

    private bool hasInitialized = false;

    #region Singleton
    public static TwitchChatClient instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    void Start()
    {
        if (!automaticInit) return;

        Init();
    }

    void Update()
    {
        if (twitchClient == null || !twitchClient.Connected) return;
        ReadChat();
    }

    public void Init()
    {
        if (hasInitialized) return;
        hasInitialized = true;

        // Checks
        if (configurationPath == "") configurationPath = Application.persistentDataPath + "/config.json";
        if (commandPrefix == "" || commandPrefix == null) commandPrefix = "!";
        if (commandPrefix.Length > 1)
        {
            Debug.LogError($"TwitchChatClient.Init :: Command prefix length should contain only 1 character. Command prefix: {commandPrefix}");
            return;
        }

        data = TwitchConfiguration.Load(configurationPath);
        if (data == null) return;
        Login();
    }

    private void Login()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + data.userToken);
        writer.WriteLine("NICK " + data.username);
        writer.WriteLine("USER " + data.username + " 8 * :" + data.username);
        writer.WriteLine("JOIN #" + data.channelName);
        writer.Flush();
    }

    private void ReadChat()
    {
        if (twitchClient.Available <= 0) return;
        var message = reader.ReadLine();

        if (!message.Contains("PRIVMSG")) return;

        var splitPoint = message.IndexOf(commandPrefix, 1);
        var username = message.Substring(0, splitPoint);

        splitPoint = message.IndexOf(":", 1);
        message = message.Substring(splitPoint + 1);

        //IMPORTANT this line prints out message
        Debug.Log(message);

        string[] messages = message.Split(' ');

        if (messages.Length == 0 /*|| messages[0][0] != commandPrefix[0]*/) return;

        /*------START OF CUSTOMIZE CODE------*/
        
        StartCoroutine(CheckEmo(messages));

        /*------END OF CUSTOMIZE CODE------*/

        username = username.Substring(1);

        TwitchChatMessage chatMessage = new TwitchChatMessage(username, messages);
        onChatMessageReceived?.Invoke(chatMessage);
    }

    public string ReadLine()
    {
        if (twitchClient.Available == 0) return "";
        return reader.ReadLine();
    }

    public void SendTwitchChatMessage(string message)
    {
        writer.WriteLine("PRIVMSG #" + data.channelName + " :/me " + message);
        writer.Flush();
    }

    public void SendCommand(string command, string parameters)
    {
        writer.WriteLine("PRIVMSG #" + data.channelName + " :" + command + " " + parameters);
        writer.Flush();
    }

    IEnumerator CheckEmo(string[] msg)
    {
        foreach (string str in msg)
        {
            string strL = str.ToLower();
            //Check for hearts, keyword = "<3"
            if (strL.IndexOf("<") != -1) //if there is a "<" character in the string
            {
                //Debug.Log("Checking for <3");

                char[] charOfStr = str.ToCharArray();
                foreach (char c in charOfStr)
                {
                    if (c.Equals('3'))
                        StartCoroutine(RandomlyDelaySendEmoji(Heart));
                }
            }
            //check for laughs, keyword = ":D"
            else if (strL.IndexOf(":d") != -1)
            {
                //Debug.Log("Checking for :D");
                //Debug.Log(":D checked: " + strL);
                StartCoroutine(RandomlyDelaySendEmoji(Laugh));
            }
            //check for boos, keyword =":\"
            else if (strL.IndexOf(":(") != -1)
            {
                //Debug.Log("Checking for :\");
                StartCoroutine(RandomlyDelaySendEmoji(Boo));
            }
            //check for awws, keyword = ":)"
            else if (strL.IndexOf("uwu") != -1)
            {
                StartCoroutine(RandomlyDelaySendEmoji(Aww));
            }
            //check for gossip, keyword = "o_O"
            else if (strL.IndexOf("o_o") != -1)
            {
                StartCoroutine(RandomlyDelaySendEmoji(Gossip));
            }
            //check for smooch, keyword = ";)"
            else if (strL.IndexOf(";)") != -1)
            {
                StartCoroutine(RandomlyDelaySendEmoji(Smooch));
            }
            //check for cheering, keyword = ">:)"
            else if (strL.IndexOf(">:)") != -1)
            {
                StartCoroutine(RandomlyDelaySendEmoji(WooHoo));
            }
            yield return null;
        }
    }

    IEnumerator RandomlyDelaySendEmoji(int i) //i is the desired emoji index
    {
        yield return new WaitForSeconds(Random.Range(0, 2f));
        sendEmojiScript.InitEmoji(i);
    }
}
