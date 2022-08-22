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

  public int playerID = -1;
  public float[] enemyPosition = new float[3];
  public bool connected = false;
  public bool readyToPlay = false;
  public bool gameOn = false;

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
      //Debug.Log("OnMessage!");
      //Debug.Log(bytes);
      var message = System.Text.Encoding.UTF8.GetString(bytes);

      if(playerID<0)
      {
        int num = Int16.Parse(System.Text.Encoding.UTF8.GetString(bytes));
        if((num%2) == 0) playerID = 2;
        else playerID = 1;
        texto.text = "Player " + playerID + " Ready";
        websocket.SendText(playerID + " Ready");
      } 
      if(message == "Begin") readyToPlay=true;
      // getting the message as a string
      if(gameOn)
      {
        parseEnemyPositions(message);
      }
      
      Debug.Log("OnMessage! " + message);
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
    if(connected && readyToPlay) gameOn=true;

  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }

  private void parseEnemyPositions(string phrase)
  { 
    string[] words = phrase.Split(' ');
    enemyPosition[0]=float.Parse(words[0]);
    enemyPosition[1]=float.Parse(words[1]);
    enemyPosition[2]=float.Parse(words[2]);
  }
}
