using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]
public class BoatController : NetworkBehaviour
{
    private NetworkIdentity identity;
    private float speed = 6F;
    private float rotateSpeed = 15.0F;

    private CharacterController characterController;
    public Transform sea;

    // Start is called before the first frame update
    void Start()
    {
        identity = this.GetComponent<NetworkIdentity>();

        if (!identity.isLocalPlayer)
        {
            transform.GetChild(0).GetChild(0).gameObject.SetActive(false); // camera
            transform.GetChild(1).gameObject.SetActive(false); // canvas
        }
        else
        {
            //  StartCoroutine(DisorientPlayer());//init coroutine function
            characterController = this.GetComponent<CharacterController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (identity.isLocalPlayer)
        {
            float horizontalCommand = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontalCommand * rotateSpeed * Time.deltaTime, 0);
            characterController.Move(transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed);
            sea.Rotate(0, -horizontalCommand * rotateSpeed * Time.deltaTime, 0);
        }
    }

    IEnumerator DisorientPlayer() //Rotate boat 10° every 3 seconds. The direction is chosen randomly each time. 
    {
        
        while (true)
        {
            float dir = Random.Range(-100, 100) / 100;
            for (int i = 0; i != 10; i++) {
                Debug.Log("proc");
                gameObject.transform.Rotate(0, (5f * dir), 0);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(5);
        }
    }
}
