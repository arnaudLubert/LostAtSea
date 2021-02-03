using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{

    /*private void Start()
    {
        // network = GameObject.FindObjectOfType<NetworkManager>();
        NetworkManager network = GameObject.FindObjectOfType<NetworkManager>();

        if (network == null)
            Debug.Log("not found");
        else
        {
            Debug.Log("found");
            network;
        }
    }*/

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
