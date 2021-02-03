using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = UnityEngine.Random;


public class StormController : MonoBehaviour
{
    public Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Thunder());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Thunder()
    {
        float baseIntensity = 0.85f;

        float TimeBetweenBurst = Random.Range(5.0F, 15.00F);
        float ThunderIntensity = Random.Range(2.0F, 5.00F);
        float ThunderDuration;
        
        int ThunderInBurst = Random.Range(1, 5);
        float BurstDuration = Random.Range(0.0F, 4.0F);
        
        while(true)
        {
            myLight.intensity = (float)baseIntensity;
            TimeBetweenBurst = Random.Range(5.0F, 15.00F);
            ThunderInBurst = Random.Range(1, 5);
            BurstDuration = Random.Range(0.0F, 4.0F);

            for (int i = 0; i <= ThunderInBurst; i++)
            {
                ThunderDuration = (float)Random.Range(0, 10) / 20;

                for (float lightIntensity = baseIntensity; lightIntensity < ThunderIntensity; lightIntensity += 0.1f) 
                {
                    myLight.intensity = (float)lightIntensity;
                }
                yield return new WaitForSeconds((float)ThunderDuration / 2F);
                for (float lightIntensity = ThunderIntensity; lightIntensity > baseIntensity; lightIntensity -= 0.1f)
                {
                    myLight.intensity = (float)lightIntensity;
                }
                yield return new WaitForSeconds((float)ThunderDuration / (float)BurstDuration);
                
                ThunderIntensity = Random.Range(0.85F, 5.00F);
            }

            yield return new WaitForSeconds((float)TimeBetweenBurst);
        }
    }
}
