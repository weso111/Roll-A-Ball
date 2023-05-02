using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whomp : MonoBehaviour
{
    public GameObject particleEffect;
    public GameObject spawnLocation;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            Debug.Log("Stomp!");
            Instantiate(particleEffect, spawnLocation.transform.position, Quaternion.Euler(-90f, 0, 0));
        }
    }
}
