using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        
    }

    void OnTriggerEnter(Collider other)
    {
        // If the other object contains the pickup tag, destroy it
        if (other.CompareTag("Pickup"))

        Destroy(other.gameObject);
    }


    
}
