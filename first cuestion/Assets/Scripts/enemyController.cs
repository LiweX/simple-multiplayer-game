using UnityEngine;

public class enemyController : MonoBehaviour
{
    public Client client;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        client = Client.instance;
        if(client.redBallToggle.isOn) ball = GameObject.FindGameObjectWithTag("blueball");
        else ball = GameObject.FindGameObjectWithTag("redball");
    }

    // Update is called once per frame
    void Update()
    {
        client.getEnemyInput();
        if(client.enemyInput.left) ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500*Time.deltaTime,0));
        if(client.enemyInput.right) ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(500*Time.deltaTime,0));
        if(client.enemyInput.up) ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,500*Time.deltaTime));  
    }
}