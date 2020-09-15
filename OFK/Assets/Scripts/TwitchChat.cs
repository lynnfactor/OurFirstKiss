using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for Twitch integration
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;
using UnityEngine.UI;

/* created by: Two Bit Circus Foundation
 *
 * This script reads messages sent through the Twitch chat and prints the messages
 *
 * We followed this tutorial to make this code: https://www.youtube.com/watch?v=v8yhy-5ntHM&list=PLlvhrv8NNgVEArGZaI2HtMTI3bj0GlhDe&index=85&t=344s
*/

public class TwitchChat : MonoBehaviour
{
    // private variables
    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    // public variables
    // get PW from twitchapps.com/tmi
    public string username, password, channelName;
    
    // reference the text object you've added to your scene
    public Text chatBox;
    
    // Start is called before the first frame update
    void Start()
    {
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        // if twitch is disconnected, reconnect it
        if(!twitchClient.Connected)
        {
            Connect();
        }

        ReadChat();

    }

    // this method will connect us to the Twitch client
    // we want to do this as soon as we start the game
    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", /*port number*/ 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        // to connect to Twitch
        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();

    }

    // read the Twitch chat
    private void ReadChat()
    {
        // if the client is available, read the message
        if(twitchClient.Available > 0)
        {
            var message = reader.ReadLine();

            // if the message was sent into the Twitch chat by a user
            if (message.Contains("PRIVMSG"))
            {
                // Get the username by splitting it from the stream
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                // Get the user's message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                
                // add this text to the chatbox and create a new line after
                chatBox.text = chatBox.text + "\n" + String.Format("{0}: {1}", chatName, message);
            }
        }
    }
}
