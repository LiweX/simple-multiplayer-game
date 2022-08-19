import asyncio
import websockets

async def echo(websocket):
    async for message in websocket:
        print(message)
        await websocket.send("Hello from Python")

async def main():
    async with websockets.serve(echo, "localhost", 8001):
        await asyncio.Future()  # run forever

asyncio.run(main())
