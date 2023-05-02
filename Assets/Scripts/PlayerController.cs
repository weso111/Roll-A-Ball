using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float baseSpeed;
    public float powerupSpeed;
    public float speed;
    int pickupCount;
    int totalPickups;
    Timer timer;
    private Rigidbody rb;
    bool wonGame = false;

    public GameObject powerupParticle;

    [Header("UI")]
    public GameObject winPanel;
    public GameObject losePanel;
    public TMP_Text winTime;
    public GameObject inGamePanel;
    public TMP_Text timerText;
    public TMP_Text pickupText;

    CameraController cameraController;

    void Start()
    {
        speed = baseSpeed;
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
        pickupCount = totalPickups;
        //Display the pickup count
        CheckPickups();
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();

        //Turn off our win panel
        winPanel.SetActive(false);
        inGamePanel.SetActive(true);
        cameraController = FindObjectOfType<CameraController>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if(cameraController.cameraStyle == CameraStyle.Free)
        {
            //rotates the player to the direction of the camrea 
            transform.eulerAngles = Camera.main.transform.eulerAngles;
            //translates the input vectors into coordinates
            movement = transform.TransformDirection(movement);
        }

        rb.AddForce(movement * speed);
        //Update the timer text to that of the timer
        timerText.text = "Time: " + timer.GetTime().ToString("F3");


    }

    void OnTriggerEnter(Collider other)
    {
        // If the other object contains the pickup tag, destroy it
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount -= 1;
            CheckPickups();
            // Get the timer object
        }
        if(other.CompareTag("Death"))
        {
            LoseGame();
        }
        if (other.CompareTag("Whomp"))
        {
            LoseGame();
        }
        if (other.CompareTag("Star"))
        {
            StartCoroutine(PowerUpTimer());
            Destroy(other.gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Death")
        {
            LoseGame();
        }
    }
    void CheckPickups()
    {
        //Debug.Log("Pickups left:" + pickupCount);
        pickupText.text = "Pickups left:" + pickupCount;

        if (pickupCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        if (wonGame == true)
            return;


        timer.StopTimer();
        //Debug.Log("You Win!!! Your time was: " + timer.GetTime().ToString("F3"));
        // inGamePanel on the next

        inGamePanel.SetActive(false);
        //Set the timer  on the text
        winTime.text = "Your time was: " + timer.GetTime().ToString("F3");
        //Turn on our win panel
        winPanel.SetActive(true);

    }

    public void LoseGame()
    {
        timer.StopTimer();
        //Debug.Log("You Win!!! Your time was: " + timer.GetTime().ToString("F3"));
        // inGamePanel on the next

        Time.timeScale = 0;

        inGamePanel.SetActive(false);
        //Set the timer  on the text
        winTime.text = "Your time was: " + timer.GetTime().ToString("F3");
        //Turn on our win panel
        losePanel.SetActive(true);
    }


    //Temporary-Remove wjen doing modules in A2
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    public void StarPowerUp()
    {

    }
   

    IEnumerator PowerUpTimer()
    {
        powerupParticle.SetActive(true);
        speed = powerupSpeed;
        yield return new WaitForSeconds(20);
        speed = baseSpeed;
        powerupParticle.SetActive(false);
    }





}