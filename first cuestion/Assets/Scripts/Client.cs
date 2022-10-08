using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Client : MonoBehaviour
{	
	[System.Serializable]
	public class PlayersList{

		[System.Serializable]
		public class Player{
			public string id;
			public string name;

			[System.Serializable]
			public class Inputs{
				public bool left;
				public bool right;
				public bool up;
			}
			public Inputs inputs;
		}
		public Player[] players;
	}




  public PlayersList players;
  [System.Serializable]
  public class Input{
	public bool left;
	public bool right;
	public bool up;
	public Input(){
		this.left=false;
		this.right=false;
		this.up=false;
	}
  }
public Input playerInput;
public Input enemyInput;
  public bool waiting = false;
  public bool connected = false;
  public bool readyToPlay = false;
  public bool gameOn = false;
  public string ip;
  public Toggle redBallToggle;
  public Text displayText;
  public string playerID; 
  public string enemyID;
  public string playerName;
  public string enemyName;

  public static Client instance;


  // Start is called before the first frame update
  private void Awake() {
    instance=this;
    DontDestroyOnLoad(this);
  }
  

  void Update()
  {
	if(waiting){
		displayText.text="Esperando otro jugador";
		checkPlayers();
		
	}
    if(connected && readyToPlay) {
		waiting=false;
		gameOn=true;
	}
  }

  	public void connectToServer (string ip,string name) {
		this.ip=ip;
		StartCoroutine(CoroutineConnect(ip,name)); 			
	}
	public void sendInput(Input input){
		StartCoroutine(CoroutineSendInput(input));
	}
	public void checkPlayers(){
		StartCoroutine(CoroutineCheckPlayers());
	}
	public void getEnemyInput(){
		StartCoroutine(CoroutineEnemyInput());
	}
	public void getPlayersList(){
		StartCoroutine(CoroutineGetPlayersList());
	}
	public void testSend(){
		StartCoroutine(CoroutineTestingSend());
	}
	private IEnumerator CoroutineConnect(string ip,string name){
		Debug.Log("Intentando conectar");
		string url = "http://" + ip + ":3000/server";
		Dictionary<string,string> form = new Dictionary<string, string>();
		form.Add("name",name);
		UnityWebRequest web = UnityWebRequest.Post(url,form);
		web.SetRequestHeader("mode","no-cors");
		yield return web.SendWebRequest();
		if(web.responseCode==201){
			Debug.Log("Conectado");
			playerID=web.downloadHandler.text;
			playerName=name;
			connected=true;
			waiting=true;
			enemyInput=new Input();
			playerInput=new Input();
		}else Debug.Log("Error al conectar");
	}

	private IEnumerator CoroutineEnemyInput(){
		string url = "http://" + ip + ":3000/server/player/"+enemyID;
		UnityWebRequest web = UnityWebRequest.Get(url);
		web.SetRequestHeader("mode","no-cors");
		yield return web.SendWebRequest();
		if(web.responseCode==200){
			enemyInput = JsonUtility.FromJson<Input>(web.downloadHandler.text);
			
		}else Debug.Log("Error en enemy input");
	}

	private IEnumerator CoroutineCheckPlayers(){
		string url = "http://" + ip + ":3000/server/players";
		UnityWebRequest web = UnityWebRequest.Get(url);
		web.SetRequestHeader("mode","no-cors");
		yield return web.SendWebRequest();
		if(web.responseCode==200){
			if(web.downloadHandler.text=="2") {
				getPlayersList();
				readyToPlay=true;
			}

		}else Debug.Log("Error en check players");
	}

	private IEnumerator CoroutineSendInput(Input input){
		
		string inputs = JsonUtility.ToJson(input);
		UnityWebRequest web = UnityWebRequest.Put("http://" + ip + ":3000/server/player/"+playerID,"{\"inputs\": "+inputs+"}");
		web.SetRequestHeader("Content-Type","application/json");
		web.SetRequestHeader("mode","no-cors");
		yield return web.SendWebRequest();
		if(web.responseCode==200){
		}else Debug.Log("Error en send input");
	
	}
	private IEnumerator CoroutineTestingSend(){
        UnityWebRequest web = UnityWebRequest.Put("http://" + ip + ":3000/server/test", "{\"test\":\"Hijo de puta\"}");
		web.SetRequestHeader("Content-Type","application/json");
        yield return web.SendWebRequest();
        if(web.responseCode==200) {
            Debug.Log(web.error);
        }
        else {
            Debug.Log("Upload complete!");
        }
	
	}
	private IEnumerator CoroutineGetPlayersList(){
		string url = "http://" + ip + ":3000/server";
		UnityWebRequest web = UnityWebRequest.Get(url);
		yield return web.SendWebRequest();
		if(web.responseCode==200){
			players = JsonUtility.FromJson<PlayersList>(web.downloadHandler.text);
			if(players.players[0].id==playerID){
				enemyID=players.players[1].id;
				enemyName=players.players[1].name;
			}else{
				enemyID=players.players[0].id;
				enemyName=players.players[0].name;
			}

		}else Debug.Log("Error en get players lists");
	}

}
  
	
