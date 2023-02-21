using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float currentTime;
    bool timing;
   
    public void StartTimer()
    {
        timing = true;
    }

    public void StopTimer()
    {
        timing = false;
    }

    public float GetTime()
    {
        return currentTime;
    }

    void Update()
    {
        if (timing)
        {
            currentTime += Time.deltaTime;
        }
    }
}
