using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDamageDealer : MonoBehaviour
{
    public int damage = 20;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponentInChildren<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}