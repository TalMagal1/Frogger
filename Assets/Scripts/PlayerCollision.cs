using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] public float forceMultiplier = 10f; 
    private Rigidbody2D playerRigidbody2D;
    private PlayerController playerController;

    void Start()
    {
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is a car
        if (collision.gameObject.CompareTag("Car"))
        {
            Vector2 carVelocity = collision.rigidbody.velocity;
            Vector2 impactForce = carVelocity * forceMultiplier;
            playerRigidbody2D.AddForce(impactForce, ForceMode2D.Impulse);
            GameManager.Instance.HandlePlayerCollision(playerController.UpwardProgress);
        }
    }
}
