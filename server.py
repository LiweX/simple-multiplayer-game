import socket

ip = "127.0.0.1"
port = 8001

server = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
server.bind((ip,port))
server.listen(5)
client, address = server.accept()
print(f"Connection Established - {address[0]}:{address[1]}")

while True:
    string = client.recv(1024)
    string = string.decode("utf-8")
    print(string)
    client.send(bytes("hello from python","utf-8"))
