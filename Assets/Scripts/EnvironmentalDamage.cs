using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;  // Required for UnityEvent

public class EnvironmentalDamage : MonoBehaviour
{
    public float countdownDuration = 5f; // Duration of the countdown in seconds
    private float countdownTimer; // Timer for the countdown
    private bool isCountingDown = false; // Flag to indicate if countdown is active
    private bool isInsideSpecificCollider = true; // Flag to indicate if player is inside the specific collider

    private PlayerHealth playerhealth;
    private double totalDamageTaken = 0;
    private int totalDamageDocumented = 0;

    void Start()
    {
        StartCountdown();
        playerhealth = this.gameObject.GetComponentInChildren<PlayerHealth>();
        Debug.Log(playerhealth);
        Debug.Log(countdownTimer);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(countdownTimer);
        // If countdown is active, decrement the timer
        if (isCountingDown)
        {
            countdownTimer -= Time.deltaTime;
            Debug.Log(countdownTimer);

            // Check if countdown has reached 0
            if (countdownTimer <= 0)
            {
                Debug.Log("HHHH");
                // Perform action when countdown reaches 0
                PerformAction(1 * Time.deltaTime);
            }
        }
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is of the specific class
        if (other.CompareTag("Safe Zone"))
        {
            // Reset the countdown if the player exits the specific collider
            ResetCountdown();
            // Set flag indicating player is outside the specific collider
            isInsideSpecificCollider = false;
        }
    }

    // OnTriggerExit is called when the Collider other has stopped touching the trigger
    void OnTriggerExit(Collider other)
    {
        // Check if the collider is of the specific class
        if (other.CompareTag("Safe Zone"))
        {
            // Set flag indicating player is inside the specific collider
            isInsideSpecificCollider = true;
            // Start the countdown if the player is inside the specific collider and the countdown is not already active
            if (!isCountingDown)
            {
                StartCountdown();
            }
        }
    }

    // Function to start the countdown
    void StartCountdown()
    {
        isCountingDown = true;
        countdownTimer = countdownDuration;
    }

    // Function to reset the countdown
    void ResetCountdown()
    {
        isCountingDown = false;
        countdownTimer = countdownDuration;
    }

    // Function to perform the action when countdown reaches 0
    void PerformAction(double num)
    {
        totalDamageTaken += num;
        if (totalDamageDocumented < (int)totalDamageTaken) {
            playerhealth.TakeDamage(1);
            totalDamageDocumented += 1;
        }
    }
}
