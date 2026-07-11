using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{
    // Values for the speed of the ball and the delay for when the ball spawns
    public float speed = 8f;
    public float launchDelay = 0.5f;

    // Range of values for where the Ball can possibly spawn
    public float minSpawnX = -10f;
    public float maxSpawnX = 10f;
    public float spawnY = 0f;

    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Random spawn position
        float randomX = Random.Range(minSpawnX, maxSpawnX);
        rb.position = new Vector2(randomX, spawnY);
        Debug.Log("Spawn Position: " + rb.position);

        //LaunchBall();
        StartCoroutine(DelayedLaunchBall());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Respawns the ball if the ball falls below the paddle/beyond the bottom boundary
        if(other.CompareTag("OutOfBounds")){
        // Random respawn position
            float randomX = Random.Range(minSpawnX, maxSpawnX);
            rb.position = new Vector2(randomX, spawnY);
            Debug.Log("Respawn Position: " + rb.position);
            //LaunchBall();
            StartCoroutine(DelayedLaunchBall());
        }
    }

    void LaunchBall()
    {
        // Float for the angle that the ball is launched at
        float angle;
        // Making random spawn angles be only to the left or right and never straight down
        if(Random.value < 0.5f){
            angle = Random.Range(210f, 250f);
        } else {
            angle = Random.Range(290f, 330f);
        }

        Vector2 direction = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;

        rb.linearVelocity = direction * speed;
    }

    IEnumerator DelayedLaunchBall()
    {
        Debug.Log("Delaying ball launch...");
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(launchDelay);
        Debug.Log("Launching ball");
        LaunchBall();
    }

    void FixedUpdate()
    {
        // Preventing acceleration of the Ball
        rb.linearVelocity = rb.linearVelocity.normalized * speed;
    }

}
