using UnityEngine;

public class playerController : MonoBehaviour
{
    public Client client;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        client = Client.instance;
        if(client.redBallToggle.isOn)ball = GameObject.FindGameObjectWithTag("redball");
        else ball = GameObject.FindGameObjectWithTag("blueball");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("left"))ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500*Time.deltaTime,0));
        if(Input.GetKeyUp("left"))client.playerInput.left=false;
        if(Input.GetKeyDown("left"))client.playerInput.left=true;
        if(Input.GetKey("right"))ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(500*Time.deltaTime,0));
        if(Input.GetKeyUp("right"))client.playerInput.right=false;
        if(Input.GetKeyDown("right"))client.playerInput.right=true;
        if(Input.GetKey("up"))ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,500*Time.deltaTime));
        if(Input.GetKeyUp("up"))client.playerInput.up=false;
        if(Input.GetKeyDown("up"))client.playerInput.up=true;
        client.sendInput(client.playerInput);
    }

}
