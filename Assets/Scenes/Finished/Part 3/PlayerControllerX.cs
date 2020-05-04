﻿/// Hint: Commenting or uncommenting in VS
/// On Mac: CMD + SHIFT + 7
/// On Windows: CTRL + K and then CTRL + C

using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 10f;
    public GameObject focalPoint;

    private bool hasPowerup;
    public float powerupStrength = 20f;
    private int powerupTime = 7;
    public GameObject powerupIndicator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        powerupIndicator.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
    }

    void FixedUpdate()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // actually use other.CompareTag("Powerup) here
        if (other.name.Contains("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            CancelInvoke("PowerupCountdown"); // if we previously picked up an powerup
            Invoke("PowerupCountdown", powerupTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        /// challenge: when other has tag "Enemy" and we have a powerup
        /// get the enemyRigidbody and push the enemy away from the player
        if (other.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = other.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.transform.position - transform.position;
            enemyRigidbody.AddForce(awayFromPlayer.normalized * powerupStrength, ForceMode.Impulse);
        }
    }

    void PowerupCountdown()
    {
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
}
