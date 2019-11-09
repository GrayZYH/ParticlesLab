using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using BestHTTP.SocketIO;
using LitJson;

public class SocketServer : MonoBehaviour
{
    public static SocketServer Instance;
    public event Action<string> OnReceiveRFIDNameEvent;

    public RenderTexture ScreenShortRT;
    [SerializeField]
    private string _hostAddress;
    private SocketManager _io;
    private Socket _socket;

    private void Start() 
    {
        Instance = this;
        //InitSocketManager();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestMutilNames();
        }
    }

    private void TestMutilNames()
    {
        string name1 = "我爱·上海";
        OnReceiveRFIDNameEvent.Invoke(name1);
        string name2 = "I' LOVE SHANG";
        OnReceiveRFIDNameEvent.Invoke(name2);
        string name3 = "i' love unity";
        OnReceiveRFIDNameEvent.Invoke(name3);
        string name4 = "i' love china";
        OnReceiveRFIDNameEvent.Invoke(name4);
        string name5 = "灰灰灰灰灰灰";
        OnReceiveRFIDNameEvent.Invoke(name5);
    }

    private void InitSocketManager()
    {
        _io = new SocketManager(new Uri(string.Format("http://{0}/socket.io/", _hostAddress)));
        _io.Open();
        _socket = _io.GetSocket();
        _socket.On(SocketIOEventTypes.Disconnect, OnDisConnectCallBack);
        _socket.On(SocketIOEventTypes.Error, OnErrorCallBack);
        _socket.On("connected", OnConnectCallBack);
        _socket.On("welcome", OnReceiveRFIDNameCallBack);
    }


    private void OnConnectCallBack(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("Connected!");
    }

    private void OnDisConnectCallBack(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("DisConnected!");
    }

    private void OnErrorCallBack(Socket socket, Packet packet, object[] args)
    {
        Debug.Log("Error");
    }

    private void OnReceiveRFIDNameCallBack(Socket socket,Packet packet,object[] args)
    {
        try
        {
            Debug.Log(args[0].ToString());
            JsonData jsonData = JsonMapper.ToObject(args[0].ToString());
            string fullname = jsonData["full_name"].ToString();
            if (OnReceiveRFIDNameEvent!=null)
            {
                OnReceiveRFIDNameEvent.Invoke(fullname);
                Debug.Log("full_name" + fullname);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
}
