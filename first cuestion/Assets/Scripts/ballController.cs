using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballController : MonoBehaviour
{
    int jumps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("left")){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500*Time.deltaTime,0)); //se multiplica por deltatime para no ser fps dependent
        }
        if(Input.GetKey("right")){
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(500*Time.deltaTime,0));
        }
        if(Input.GetKeyDown("up") && jumps>0){
            jumps--;
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,100));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "floor"){
            jumps = 3;
        }
    }
}
