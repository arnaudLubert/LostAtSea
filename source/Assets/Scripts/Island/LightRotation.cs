using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public float rotateSpeed = 360.0F;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Rotate");
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    // Update is called once per frame
    IEnumerator Rotate()
    {
        for (;;)
        {
            transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            yield return new WaitForSeconds(.01f);
        }
    }
}
