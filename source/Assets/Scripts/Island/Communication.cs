using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Communication : NetworkBehaviour
{
    //public NetworkIdentity identity;
    public MorseCode radio;
    public Canvas gui;

    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendMsg(string message)
    {
        CmdMessage(message);

    }

    [Command]
    public void CmdMessage(string message)
    {
        // Server say all clients, your message
        RpcMessage(message);
    }
    [ClientRpc]
    public void RpcMessage(string message)
    {
        GameObject fisher = GameObject.FindGameObjectWithTag("Fisher");

        if (fisher != null)
        {
            fisher.GetComponent<Communication>().receivedMsg(message);
        }
    }

    public void receivedMsg(string message)
    {
        radio.ReadMessage(message);

        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine("writeText", message);
    }

    private IEnumerator writeText(string msg)
    {
        Text speaker = gui.transform.GetChild(0).GetComponent<Text>();
        Text text = gui.transform.GetChild(1).GetComponent<Text>();
        speaker.gameObject.SetActive(true);
        text.text = "";

        foreach (char c in msg)
        {
           /* if (Random.Range(0, 10) == 3) // bad communication sim
                msg.text.text += '□';
            else*/
                text.text += c;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(5);

        while (text.text.Length != 0)
        {
            text.text = text.text.Substring(1);
            yield return new WaitForSeconds(0.5f);
        }
        speaker.gameObject.SetActive(false);
        yield return null;
    }
}
