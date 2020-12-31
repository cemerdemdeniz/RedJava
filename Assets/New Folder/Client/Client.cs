 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Client : MonoBehaviour
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
    private int connectionId;

    // Logic
    private byte error;
    private byte[] buffer = new byte[Buffer_Sıze];
    private bool isConnected;
    private void Start()
    {
        GlobalConfig config = new GlobalConfig();

        NetworkTransport.Init(config);


        //Host Topology
        ConnectionConfig cc = new ConnectionConfig();

        reliableChannelId = cc.AddChannel(QosType.Reliable);
        unreliableChannelId = cc.AddChannel(QosType.Unreliable);
        HostTopology topo = new HostTopology(cc, Max_Connectıons);


        // Connection hosts
        hostId = NetworkTransport.AddHost(topo, 0);

#if UNITY_WEBGL

        //webGl Client
        connectionId = NetworkTransport.Connect(hostId,Server_ıp, Web_port, 0, out error);
#else


        // Standalone Client
        NetworkTransport.Connect(hostId, Server_ıp, Server_Port, 0, out error);
#endif
    }
}
