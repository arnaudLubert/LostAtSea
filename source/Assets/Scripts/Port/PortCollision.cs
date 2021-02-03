using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

        void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("port"))
            SceneManager.LoadScene(2);
        else if (other.CompareTag("loose"))
            SceneManager.LoadScene(3);
    }
}
