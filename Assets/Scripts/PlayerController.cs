using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    int pickupCount;
    Timer timer;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Display the pickup count
        CheckPickups();
        timer = FindObjectOfType<Timer>();
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
        {
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount = -1;
            CheckPickups();
            // Get the timer object
        }
    }

    void CheckPickups()
    {
        Debug.Log("Pickups left:" + pickupCount);


        if (pickupCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        timer.StopTimer();
        Debug.Log("You Win!!! Your time was: " + timer.GetTime().ToString("F3"));
    }

   

}
