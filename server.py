from ast import Global
from asyncio.windows_events import NULL
from email import message
from imp import NullImporter
from websocket_server import WebsocketServer

class Players:
    player1 = False
    player2 = False
    gameOn = False
    client1 = NULL
    client2 = NULL

players = Players()

# Called for every client connecting (after handshake)
def new_client(client, server):
    print("New client connected and was given id %d" % client['id'])
    server.send_message(client,str(client['id']))


# Called for every client disconnecting
def client_left(client, server):
    print("Client(%d) disconnected" % client['id'])


# Called when a client sends a message
def message_received(client, server, msg):
    if len(msg) > 200:
        msg = msg[:200]+'..'
    print("Client(%d) said: %s" % (client['id'], msg))
    if msg == "1 Ready": 
        players.player1=True
        players.client1=client
    if msg == "2 Ready": 
        players.player2=True
        players.client2=client
    if players.player1 == True and players.player2 == True: 
        server.send_message_to_all("Begin")
        players.player1 = False
        players.player2 = False
        players.gameOn = True
    if players.gameOn == True:
        if players.client1['id'] == client['id']:
            server.send_message(players.client2,msg)
        if players.client2['id'] == client['id']:
            server.send_message(players.client1,msg)    






PORT=9001
server = WebsocketServer(port = PORT)
server.set_fn_new_client(new_client)
server.set_fn_client_left(client_left)
server.set_fn_message_received(message_received)
server.run_forever()