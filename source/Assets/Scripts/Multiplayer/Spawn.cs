using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        /*GameObject spaw = GameObject.FindGameObjectWithTag("spawn");
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");

        int fisher = spawners[0].name == "Spawn Fisher" ? 0 : 1;
        //Debug.Log(transform.position);
        if (this.transform.CompareTag("Fisher"))
        {
            spaw.transform.position = spawners[fisher].transform.position;
            this.transform.position = spawners[fisher].transform.position;
        }
        else
        {
            spaw.transform.position = spawners[fisher == 1 ? 0 : 1].transform.position;
            this.transform.position = spawners[fisher == 1 ? 0 : 1].transform.position;
        }*/
       // spaw.transform.position = new Vector3(0, 0, 0);
       // this.transform.position = new Vector3(0, 0, 0);
       // spawn.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
