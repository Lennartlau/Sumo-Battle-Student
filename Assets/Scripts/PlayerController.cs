/// Hint: Commenting or uncommenting in VS
/// On Mac: CMD + SHIFT + 7
/// On Windows: CTRL + K and then CTRL + C

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 10f;
    public GameObject focalPoint;

    public bool hasPowerup;
    public float powerupStrength = 15f;
    private int powerupTime = 7;
    //public GameObject powerupIndicator;

    void Start()
    {
        // looks in the (current) gameObject for component of type Rigidbody
        // gameObject.GetComponent<Rigidbody>() would work as well
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //powerupIndicator.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
    }

    // FixedUpdate is called on a fixed physics loop
    void FixedUpdate()
    {
        // gets the vertical user input from joystick/keyboard (w/s or up/down)
        // as a float between -1 and 1
        float forwardInput = Input.GetAxis("Vertical");
        // gets the normalized forward direction of the focal point aka camera
        // normalized means values of the vector range between -1 and 1
        Vector3 forwardDirection = focalPoint.transform.forward.normalized;
        // adds force to the player in the direction the camera is facing
        playerRb.AddForce(forwardDirection * forwardInput * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            //powerupIndicator.gameObject.SetActive(true);
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
        if (true)
        {
            Rigidbody enemyRigidbody;
            Vector3 awayFromPlayer;
            //enemyRigidbody.AddForce(..., ForceMode.Impulse);
        }
    }

    void PowerupCountdown()
    {
        hasPowerup = false;
        //powerupIndicator.gameObject.SetActive(false);
    }
}
