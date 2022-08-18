using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("left")){
            gameObject.transform.Translate(-10 * Time.deltaTime,0,0);
        }
        if(Input.GetKey("right")){
            gameObject.transform.Translate(10 * Time.deltaTime,0,0);
        }
    }
}
