/*
 * This code has been 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.IO;

public class TwitchChat : MonoBehaviour
{
    private TcpClient twitchClient;
    private StreamReader sReader;
    private StreamWriter sWriter;

    private string username = "ourfirstkiss";
    [SerializeField] string password;
    private string channelName = "outfirstkiss";


    void Start()
    {
        Connect();
    }
    void Update()
    {
        if (twitchClient == null || !twitchClient.Connected)
        {
            Connect();
        }
        ReadChat();
    }

    void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        sReader = new StreamReader(twitchClient.GetStream());
        sWriter = new StreamWriter(twitchClient.GetStream());

        sWriter.WriteLine("PASS " + password);
        sWriter.WriteLine("NICK " + username);
        sWriter.WriteLine("USER " + username + " 8 * :" + username);
        sWriter.WriteLine("JOIN #" + channelName);
        sWriter.Flush();
    }

    void ReadChat()
    {

        if (twitchClient.Available > 0)
        {
            var message = sReader.ReadLine(); //read the current message

            if (message.Contains("PRIVMSG"))
            {
                //Get the user's name by splitting it from the string
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                //Get the user's message by splitting it from the string
                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                Debug.Log(String.Format("{0}: {1}", chatName, message));
            }

            Debug.Log(message);
        }
        else
        {
            //Debug.Log(twitchClient.ToString());
            //Debug.Log(twitchClient.Available);
        }
    }
}
