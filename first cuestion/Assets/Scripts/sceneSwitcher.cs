using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using HybridWebSocket;
public class sceneSwitcher : MonoBehaviour
{
    private inputReader ipObject;
    private string ip;
    public void ChangeScene(int sceneID){

        getIPfromInputField();
        Debug.Log(ip);

        // Create WebSocket instance
        WebSocket ws = WebSocketFactory.CreateInstance("ws://" + ip + ":8001");

        // EVENT LISTENERS
        ws.OnOpen += () =>
        {
            Debug.Log("WS connected!");
            Debug.Log("WS state: " + ws.GetState().ToString());

            ws.Send(Encoding.UTF8.GetBytes("Hello from Unity 3D!"));
            
        };
        ws.OnMessage += (byte[] msg) =>
        {
            Debug.Log("WS received message: " + Encoding.UTF8.GetString(msg));
        };

        ws.OnError += (string errMsg) =>
        {
            Debug.Log("WS error: " + errMsg);
        };

        ws.OnClose += (WebSocketCloseCode code) =>
        {
            Debug.Log("WS closed with code: " + code.ToString());
        };

        // Connect to the server
        ws.Connect();
        

        
    }

    public void getIPfromInputField(){

        GameObject go = GameObject.Find("ip");
        ipObject = go.GetComponent<inputReader>();
        ip = ipObject.ip;

    }

    public void changeScene(){
        SceneManager.LoadScene(1);
    }
}
