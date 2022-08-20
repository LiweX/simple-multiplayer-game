using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NativeWebSocket;

public class buttonController : MonoBehaviour
{
    public Text messageToSend;
    public Text textOnScreen;
    public Client server;


    public void trySendMessage(){
        string msg = messageToSend.text;
        Debug.Log(msg);
        server.websocket.Send(Encoding.UTF8.GetBytes(msg));

    }
    public void changeScene(){
        SceneManager.LoadScene(1);
    }
}
