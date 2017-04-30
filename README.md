# FUDI
Simple FUDI compliant networking protocol for C#

FUDI is a networking protocol used by the Pure Data patching language invented by Miller Puckette. It is a string based protocol in which messages are separated by semicolons. Messages are made up of tokens separated by whitespaces, and numerical tokens are represented as strings.

[More info on FUDI (wiki)]( https://en.wikipedia.org/wiki/FUDI )

Currently I've only implemented UDP, I'll be adding TCP shortly.

## Send example
```c#
Karanko.FUDI.Netsend send = new Karanko.FUDI.Netsend(3001);
send.Connect();
if (send.Connected())
{
    send.Message("send a string");
    send.Message(567890);
    send.Message(new List<string>() { "One","Two","Three"});
    send.Message(new List<int>() {1,2,3});
}
 ```
 
## Receive example
```c#
void Main()
{
    Karanko.FUDI.Netreceive receive = new Karanko.FUDI.Netreceive(9090);
    receive.OnReceive += new EventHandler(DoWork);
    receive.Connect();

    Console.ReadLine();
    receive.Disconnect();
}
void DoWork(object s, EventArgs e)
{
    string message = (string)s;
    Console.WriteLine(message);
}
 ```
