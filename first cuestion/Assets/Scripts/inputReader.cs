using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputReader : MonoBehaviour
{
    private string ip;
    public void readInputField(string s){
        ip=s;
        Debug.Log(ip);
    }
}
