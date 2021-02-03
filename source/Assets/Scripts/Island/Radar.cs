using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public GameObject line, prefabPoint, prefabBoat;
    public Transform obstaclesPool;
    private float lineSpeed = 70.0f;
    private const float xRatio = 0.005f;
    private const float yRatio = 0.004f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("rotateLine");
        StartCoroutine("scanMapLoop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ScanMap()
    {
        foreach (Transform child in obstaclesPool.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        GameObject point;
        Vector3 position;

        // radarSize = [-5, 5]
        // mapSize = x[-695, 715] y[-839, 837];
        foreach (GameObject obstacle in obstacles)
        { 
            position = new Vector3(obstacle.transform.position.x * xRatio, 0.1f, obstacle.transform.position.z * yRatio);
            point = Instantiate(prefabPoint, obstaclesPool);
            point.transform.position = new Vector3(obstaclesPool.position.x + position.x * transform.localScale.x, obstaclesPool.position.y + position.y * transform.localScale.y, obstaclesPool.position.z + position.z * transform.localScale.z);
        }
        GameObject port = GameObject.FindGameObjectWithTag("port");

        if (port != null)
        {
            position = new Vector3(port.transform.position.x * xRatio, 0.1f, port.transform.position.z * yRatio);
            point = Instantiate(prefabBoat, obstaclesPool);
            point.transform.position = new Vector3(obstaclesPool.position.x + position.x * transform.localScale.x, obstaclesPool.position.y + position.y * transform.localScale.y, obstaclesPool.position.z + position.z * transform.localScale.z);
        }
        GameObject fisher = GameObject.FindGameObjectWithTag("Fisher");

        if (fisher != null)
        {
            position = new Vector3(fisher.transform.position.x * xRatio, 0.1f, fisher.transform.position.z * yRatio);
            point = Instantiate(prefabBoat, obstaclesPool);
            point.transform.position = new Vector3(obstaclesPool.position.x + position.x * transform.localScale.x, obstaclesPool.position.y + position.y * transform.localScale.y, obstaclesPool.position.z + position.z * transform.localScale.z);
        }
    }

    private IEnumerator rotateLine()
    {
        while (true)
        {
            line.transform.Rotate(0, lineSpeed * Time.deltaTime, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator scanMapLoop()
    {
        while (true)
        {
            ScanMap();
            yield return new WaitForSeconds(4);
        }
    }
}
