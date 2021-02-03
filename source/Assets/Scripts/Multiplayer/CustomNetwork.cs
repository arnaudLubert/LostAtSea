using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class CustomNetwork : NetworkManager
{
    public int chosenCharacter = 0;
    public GameObject[] characters;
    private List<NetworkConnection> clients;
    private NetworkConnection[] roles;

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }

    public class StringNetworkMessage : MessageBase
    {
        public string data;
    }


    public void Start()
    {
        clients = new List<NetworkConnection>();
        roles = new NetworkConnection[2];
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        if (clients.Count < 1)
            return;
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        chosenCharacter = message.chosenClass;

        if (roles[chosenCharacter] != null && roles[chosenCharacter].isReady)
            chosenCharacter = chosenCharacter == 0 ? 1 : 0;
        roles[chosenCharacter] = conn;

        GameObject spaw = GameObject.FindGameObjectWithTag("spawn");
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
        int fisher = spawners[0].name.Equals("Spawn Fisher") ? 0 : 1;

        if (chosenCharacter == 1)
            spaw.transform.position = spawners[fisher].transform.position;
        else
            spaw.transform.position = spawners[fisher == 1 ? 0 : 1].transform.position;
        
        GameObject player;
        Transform startPos = GetStartPosition();
        if (startPos != null)
        {
            player = Instantiate(characters[chosenCharacter], startPos.position, startPos.rotation);
        }
        else
        {
            player = Instantiate(characters[chosenCharacter], Vector3.zero, Quaternion.identity);

        }
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

/*        StringNetworkMessage messageContainer = new StringNetworkMessage();
        messageContainer.data = "Hello server!";
        client.Send(123, messageContainer);*/
    }

    public void OnMessageReceived(StringNetworkMessage netMsg)
    {
        Debug.Log("Message received: " + netMsg.data);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {     
        NetworkMessage msg = new NetworkMessage();
        msg.chosenClass = chosenCharacter;



        clients.Add(conn);
        ClientScene.AddPlayer(conn, 0, msg);


      //  NetworkServer.RegisterHandler(123, OnMessageReceiv);
    }
/*
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("Client has started");
        //StartCoroutine("sendmsg", client);
       // client.RegisterHandler(10, OnServerReadyToBeginMessage);
       //  client.RegisterHandler(MsgType.Connect, OnConnected);

    }*/

    private IEnumerator sendmsg(NetworkClient client)
    {

        yield return new WaitForSeconds(2);
  
        yield return null;
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        int id = -1;

        for (int i = 0; i != clients.Count; i++)
            if (clients[i].Equals(conn))
            {
                id = i;
                break;
            }

        if (id != -1)
            clients.RemoveAt(id);

        NetworkServer.DestroyPlayersForConnection(conn);

        // The last person in the game left.
        if (clients.Count < 1)
        {
            // Forces the server to shutdown.
          //  Shutdown();

            // Reset internal state of the server and start the server again.
            //Start();
        }
    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
    }

    public void pickCharacter(int position)
    {
        chosenCharacter = position;
    }
}
