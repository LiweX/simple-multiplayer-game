using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputReader : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(this);

    }
    public string ip;
    public void readInputField(string s){
        ip=s;
    }
}
