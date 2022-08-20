using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneSwitcher : MonoBehaviour
{
    private inputReader ipObject;
    private string ip;

    public void tryConnect(int sceneID){

        getIPfromInputField();
        Debug.Log(ip);
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
