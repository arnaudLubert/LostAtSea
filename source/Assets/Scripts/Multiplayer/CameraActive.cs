using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class CameraActive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("checkCameras");

    }

    private IEnumerator checkCameras() {
        yield return new WaitForSeconds(0.4f);

        Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
        bool active = false;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].gameObject.activeInHierarchy)
            {
                active = true;
                break;
            }
        }

        if ( !active)
            SceneManager.LoadScene(0);
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
