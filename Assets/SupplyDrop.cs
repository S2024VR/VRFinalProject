using UnityEngine;

public class SupplyDrop : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            // Do something
            Debug.Log("Player has collected the supply drop!");

            // Trigger the event
            GameEvents.current.SupplyDropCollected();
            Debug.Log("Event triggered");

            // Destroy the supply drop
            Destroy(gameObject);
        }
    }
}
