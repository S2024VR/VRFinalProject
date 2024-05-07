using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Working!");
            // PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(damage);
            // }
        }
    }
}
