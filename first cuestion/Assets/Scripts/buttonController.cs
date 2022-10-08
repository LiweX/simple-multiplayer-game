using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class buttonController : MonoBehaviour
{
    public Text inputfieldIP;
    public Text inputfieldName;
    public Text textOnScreen;
    public Client server;


    public void buttonPress(){
        if(!server.connected) {
            string ip = inputfieldIP.text;
            string playerName = inputfieldName.text;
            server.connectToServer(ip,playerName);
        }
    }
    private void Update() {
        if(server.gameOn)  changeScene();
    }
    public void changeScene(){
        SceneManager.LoadScene(1);
    }
}
