using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NativeWebSocket;

public class buttonController : MonoBehaviour
{
    public Text inputfield;
    public Text textOnScreen;
    public Client server;


    public void buttonPress(){
        string ip = inputfield.text;
        Debug.Log(ip);
        server.tryConnect(ip);

    }
    private void Update() {
        if(server.connected && server.readyToPlay)  changeScene();
    }
    public void changeScene(){
        SceneManager.LoadScene(1);
    }
}
