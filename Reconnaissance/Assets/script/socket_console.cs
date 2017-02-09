using UnityEngine;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using System;
using System.Xml;
using System.IO;

public class socket_console : MonoBehaviour
{
    string ip_address;
    public static int scene_index;
    scene_controller scene_controller;
    WebSocket _ws;
    bool Alert = false;
    bool _over = false;
    image_change image_change;
    GameObject ErrorText;
    public static int rader_index;
    int i = 0;
    void Awake()
    {
        //xmlpath
        string filepath = Application.dataPath + "/StreamingAssets/setting.xml";
        XmlDocument xmlDoc2 = new XmlDocument();
        if (File.Exists(filepath))
        {

            xmlDoc2.Load(filepath);
            XmlNodeList ipList = xmlDoc2.GetElementsByTagName("ip");

            foreach (XmlNode comportInfo in ipList)
            {

                ip_address = comportInfo.InnerText;
                Debug.Log(ip_address);
            }

        }
        else
        {
            Debug.Log("file not find");
        }
    }

    // Use this for initialization
    void Start()
    {
        Connect();

    }

    // Update is called once per frame
    void Update()
    {
        scene_controller = GameObject.Find("scene_controller").GetComponent<scene_controller>();
        if (_over)
        {
            SceneManager.LoadSceneAsync("success");
            reciprocal.coolingDown = true;
            _over = false;
        }
        if (Alert)
        {

            ErrorText = (GameObject)Instantiate(Resources.Load("ErrorText"));

        }
        if (ErrorText)
        {
            Alert = false;
        }

    }

    public void play_movie(string i)
    {
        Send(i);
    }

    public void TimeOut()
    {
        Send("idle");
        SceneManager.LoadSceneAsync("title");
    }

    


    #region websocket_sharp function
    void Connect()
    {
        try
        {
            _ws = new WebSocket(ip_address);

            _ws.OnOpen += _ws_OnOpen;
            _ws.OnMessage += _ws_OnMessage;
            _ws.OnError += _ws_OnError;
            _ws.OnClose += _ws_OnClose;


            _ws.ConnectAsync();
        }
        catch (Exception ex)
        {
            Debug.Log("Connect, ex = " + ex.Message);
        }
    }
    private void _ws_OnOpen(object sender, EventArgs e)
    {
        Debug.Log("OnOpen");

    }
    private void _ws_OnMessage(object sender, MessageEventArgs e)
    {
        Debug.Log("Recv: " + e.Data);
        switch (e.Data)
        {
            /*case "rader_0":
                rader_index = 0;
                break;
            case "rader_1":
                rader_index = 1;
                break;
            case "rader_2":
                rader_index = 2;
                break;*/
            case "complete":
                _over = true;            
                break;
            default:
                if(int.TryParse(e.Data, out i))
                {
                    rader_index = int.Parse(e.Data);
                }
                break;
        }
    }

    private void _ws_OnClose(object sender, CloseEventArgs e)
    {
        Alert = true;
        Debug.Log("OnClose, " + e.Reason);
    }

    private void _ws_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
    {
        Alert = true;
        Debug.Log("OnError, " + e.Message);

    }

    void Send(string msg)
    {
        Debug.Log("Send: " + msg);

        try
        {
            _ws.SendAsync(msg, OnSendCompleted);
        }
        catch (Exception ex)
        {
            Debug.Log("Send, ex = " + ex.Message);
        }
    }

    private void OnSendCompleted(bool result)
    {
        Debug.Log("OnSendCompleted: " + result);
    }
    #endregion
}


