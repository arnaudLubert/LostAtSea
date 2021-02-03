using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(CharacterController))]
public class GuardianController : NetworkBehaviour
{
    private NetworkIdentity identity;

    private CharacterController characterController;
    private Camera camera;

    private const float cameraLimit = 45.0F;

    private const float speed = 6.0F;
    private const float rotateSpeed = 2.0F;

    private const float gravity = 9.81F;
    private const float jumpForce = 3.0F;

    private Vector3 inertia = Vector3.zero;

    public float verticalAcceleration = 0.0F;
    public float verticalSpeed = 0.0F;

    // Start is called before the first frame update
    void Start()
    {
        identity = this.GetComponent<NetworkIdentity>();
        Cursor.visible = false;

        if (!identity.isLocalPlayer)
        {
            transform.GetChild(0).gameObject.SetActive(false); // camera
            transform.GetChild(1).gameObject.SetActive(false); // canvas
        }

        characterController = this.GetComponent<CharacterController>();
        camera = this.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (identity.isLocalPlayer)
        {
            // WASD/ZQSD keys and left stick
            float horizontalMove = Input.GetAxis("Horizontal");
            float verticalMove = Input.GetAxis("Vertical");

            // Mouse and right stick
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");
            
            // Jump
            bool jumpCommand = Input.GetButtonDown("Jump");

            // Apply rotation
            transform.Rotate(0, horizontalRotation * rotateSpeed, 0);
            camera.transform.Rotate(verticalRotation * rotateSpeed, 0, 0);

            // Limit vertical rotation
            Vector3 cameraRotation = camera.transform.localEulerAngles;
            if (cameraRotation.x > 180.0F)
                cameraRotation.x -= 360.0F;
            if (cameraRotation.x > cameraLimit)
                cameraRotation.x = cameraLimit;
            if (cameraRotation.x < -cameraLimit)
                cameraRotation.x = -cameraLimit;
            camera.transform.localEulerAngles = cameraRotation;

            // Translation
            Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;

            // Gravity
            Vector3 g = transform.up * -gravity;
            if (characterController.isGrounded)
            {
                verticalSpeed = -gravity;
                verticalAcceleration = 0;

                if (jumpCommand)
                {
                    Debug.Log("Jump!");
                    verticalAcceleration = 0;
                    verticalSpeed = jumpForce;

                    inertia = move;

                    g = transform.up * verticalAcceleration;
                }

                // Apply translation
                characterController.Move(move * Time.deltaTime * speed + g * Time.deltaTime);
            }
            else
            {
                verticalAcceleration -= gravity * Time.deltaTime;
                verticalSpeed += verticalAcceleration * Time.deltaTime;

                g = transform.up * verticalSpeed;

                // Apply translation
                characterController.Move(inertia * Time.deltaTime * speed + g * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.visible = !Cursor.visible;
        }

    }

    /*
    void AncienneVersion()
    {
        if (identity.isLocalPlayer)
        {
            float horizontalCommand = Input.GetAxis("Horizontal");
            float verticalCommand = Input.GetAxis("Vertical");

            // Rotate around y axis
            transform.Rotate(0, horizontalCommand * rotateSpeed, 0);

            // Move forward / backward
            Vector3 move = verticalCommand * transform.TransformDirection(Vector3.forward);

            // Move jump / fall
            if (characterController.isGrounded && Input.GetButtonDown("Jump"))
            {
                Debug.Log("Jump!");
                move += jumpHeight * transform.TransformDirection(Vector3.up);
            }

            if (!characterController.isGrounded)
            {
                move += Physics.gravity.y * transform.TransformDirection(Vector3.up) * 0.1F;
            }

            characterController.Move(move * Time.deltaTime * speed);
        }
    }
    //*/
}
