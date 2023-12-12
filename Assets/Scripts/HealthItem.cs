using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null && playerHealth.currentHealth < playerHealth.maxHealth)
            {
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                playerHealth.GainHealth();
                Destroy(gameObject);
            }
        }
    }
}
