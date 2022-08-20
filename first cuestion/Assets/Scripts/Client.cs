using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using NativeWebSocket;

public class Client : MonoBehaviour
{
  public WebSocket websocket;
  public Text texto;

  public bool connected = false;

  // Start is called before the first frame update
  private void Awake() {
    DontDestroyOnLoad(this);
  }
  async public void tryConnect(string ip)
  {
    websocket = new WebSocket("ws://" + ip + ":9001");

    websocket.OnOpen += () =>
    {
      Debug.Log("Connection open!");
      connected = true;
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
    };

    websocket.OnClose += (e) =>
    {
      Debug.Log("Connection closed!");
      connected = false;
    };

    websocket.OnMessage += (bytes) =>
    {
      Debug.Log("OnMessage!");
      //Debug.Log(bytes);
      texto.text = System.Text.Encoding.UTF8.GetString(bytes);

      // getting the message as a string
      // var message = System.Text.Encoding.UTF8.GetString(bytes);
      // Debug.Log("OnMessage! " + message);
    };

    // Keep sending messages at every 0.3s
    //InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

    // waiting for messages
    await websocket.Connect();
  }

  void Update()
  {
    if(connected){
      #if !UNITY_WEBGL || UNITY_EDITOR
      websocket.DispatchMessageQueue();
      #endif
    }

  }

  async void SendWebSocketMessage()
  {
    if (websocket.State == WebSocketState.Open)
    {
      // Sending bytes
      await websocket.Send(new byte[] { 10, 20, 30 });

      // Sending plain text
      await websocket.SendText("plain text message");
    }
  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }

}
