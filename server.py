from simple_websocket_server import WebSocketServer, WebSocket


class ServerHandlers(WebSocket):
    def handle(self):
        cuenta = self.data.decode()
        resultado = eval(cuenta)
        print(cuenta)
        print(resultado)
        self.send_message(bytes(str(resultado),"utf-8"))

    def connected(self):
        print(self.address, 'connected')

    def handle_close(self):
        print(self.address, 'closed')


server = WebSocketServer('', 9001, ServerHandlers)
server.serve_forever()
