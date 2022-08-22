using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ballController : MonoBehaviour
{
    int jumps=0;
    public Client client;
    public GameObject ball;

    public GameObject enemy;
    string position;
    string rotation;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("WebSocketClient");
        if(client.playerID == 1)
        {
            ball = GameObject.Find("redball");
            enemy = GameObject.Find("blueball");
        }
        if(client.playerID == 2)
        {
            ball = GameObject.Find("blueball");
            enemy = GameObject.Find("redball");
        }
        client = go.GetComponent<Client>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey("left")){
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500*Time.deltaTime,0)); //se multiplica por deltatime para no ser fps dependent
        }
        if(Input.GetKey("right")){
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(500*Time.deltaTime,0));
        }
        if(Input.GetKeyDown("up") && jumps>0){
            jumps--;
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,100));
        }
        client.websocket.SendText(GetPosition()+" "+GetRotation()+" "+client.playerID);
        SetEnemyPosition();
        
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "floor"){
            jumps = 3;
        }
    }

    private string GetPosition(){
        string xaxis = ball.transform.position.x.ToString();
        string yaxis = ball.transform.position.y.ToString();
        position = xaxis + " " + yaxis;
        return position;
    }
    private string GetRotation(){ 
        rotation = ball.transform.rotation.z.ToString();
        return rotation;
    }
    private void SetEnemyPosition(){
        Vector3 position = new Vector3(client.enemyPosition[0],client.enemyPosition[1],0);
        Quaternion rotation = new Quaternion(0,0,client.enemyPosition[2],0);
        enemy.GetComponent<Transform>().SetPositionAndRotation(position,rotation);
    }
}
