using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    //const
    private const int Max_Connectıons = 100;
    private string Server_ıp = "125.1.0.1";
    private const int Server_Port = 8999;
    private const int Web_port = 8998;
    private const int Buffer_Sıze = 1024;

    //channels
    private int reliableChannelId;     //purchase an item 
    private int unreliableChannelId;    //updating movement of other playeyers

    // Host
    private int hostId;
    private int webHostId;
    //Logic
    private bool isInit;
    private byte[] buffer = new byte[Buffer_Sıze];


    private void Start()
    {
        GlobalConfig config = new GlobalConfig();

        NetworkTransport.Init(config);


        //Host Topology
        ConnectionConfig cc = new ConnectionConfig();

       reliableChannelId = cc.AddChannel(QosType.Reliable);
       unreliableChannelId = cc.AddChannel(QosType.Unreliable);
        HostTopology topo = new HostTopology(cc, Max_Connectıons);


        // Adding hosts
        hostId = NetworkTransport.AddHost(topo, Server_Port);
        webHostId = NetworkTransport.AddWebsocketHost(topo, Web_port);

        isInit = true;

    }
    private void Update()
    {
        if (isInit)
            return;
        int outHostId, outConnectionId, outChannelId;
        int receivedSize;
        byte error;

       NetworkEventType e = NetworkTransport.Receive(out outChannelId, out outConnectionId, out outChannelId,buffer ,buffer.Length , out receivedSize, out error);
    if (e != NetworkEventType.Nothing)
        {
            //Ther is no message , let's stop here
            return;
        }

        switch (e)
        {
            case NetworkEventType.ConnectEvent:
                {
                    break;
                }
            case NetworkEventType.DisconnectEvent:
                {
                    break;
                }
            case NetworkEventType.DataEvent:
                {
                    break;
                }
            case NetworkEventType.BroadcastEvent:
                {
                    break;
                }
            case NetworkEventType.Nothing:
                {
                    return;
                    
                }










        }
    
    
    
    }





}
