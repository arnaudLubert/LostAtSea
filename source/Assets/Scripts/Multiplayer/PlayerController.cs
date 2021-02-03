using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public CharacterController characterController;
    public NetworkIdentity identity;
    private readonly float speed = 5;

    // Start is called before the first frame update
    void Start()
    {

        if ( !identity.isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
        // _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (identity.isLocalPlayer)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            characterController.Move(move * Time.deltaTime * speed);
        }
    }

    /*public override void OnStartLocalPlayer()
    {
        // local player only
    }*/
}
