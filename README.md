# ExchangeNetCore
A sample demonstrates the difference between monolithic and microservices architecture

<img src="https://github.com/trietdvn/ExchangeNetCore/blob/master/ExchangeNetCoreAppDemo.PNG" width="650" height="481"/>

Use Cases
- Multiple users are using exchange for trading 
- Exchange receives buy/sell orders  
- Exchange matches orders between buyers and sellers
- Exchange broadcasts updated order book to all users

Technical Stack
- Asp.net Core 2.2
- Api Gateway: Ocelot
- Event Bus: RabbitMQ
- WebSocket: SignalR
