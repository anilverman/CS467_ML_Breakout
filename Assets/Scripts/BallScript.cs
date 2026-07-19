using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{
    // Values for the speed of the ball and the delay for when the ball spawns
    public float speed = 8f;
    public float launchDelay = 0.5f;

    // Prevent the ball from becoming too horizontal or vertical
    public float minimumVerticalSpeed = 2f;
    public float minimumHorizontalSpeed = 1f;

    // Range of values for where the Ball can possibly spawn
    public float minSpawnX = -10f;
    public float maxSpawnX = 10f;
    public float spawnY = 0f;
    // public float timeNoHit = 8f;
    // private float timeSinceHit = 0f;

    private Rigidbody2D rb;
    private ScoreScript scoreScript;
    private PaddleAgent paddleAgent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreScript = FindFirstObjectByType<ScoreScript>();
        paddleAgent = FindFirstObjectByType<PaddleAgent>();
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
        if (other.CompareTag("OutOfBounds"))
        {
            if (paddleAgent != null)
            {
                // Training scene:
                // punish the agent and let OnEpisodeBegin reset the ball.
                paddleAgent.HandleBallOutOfBounds();
            }
            else
            {
                // Normal game scene:
                scoreScript.LoseLife();

                // Random respawn position
                float randomX = Random.Range(minSpawnX, maxSpawnX);
                rb.position = new Vector2(randomX, spawnY);

                Debug.Log("Respawn Position: " + rb.position);
                StartCoroutine(DelayedLaunchBall());
            }
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle") || collision.gameObject.CompareTag("Brick"))
        {
            timeSinceHit = 0f;
        }
    }
    */

    void LaunchBall()
    {
        // Float for the angle that the ball is launched at
        float angle;
        // Making random spawn angles be only to the left or right and never straight down
        if (Random.value < 0.5f)
        {
            // angle = Random.Range(210f, 250f);
            angle = 225f;
        }
        else
        {
            // angle = Random.Range(290f, 330f);
            angle = 315f;
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

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;

        // Do nothing while the ball is waiting to launch.
        if (velocity.sqrMagnitude < 0.001f)
        {
            return;
        }

        // Prevent nearly horizontal movement.
        if (Mathf.Abs(velocity.y) < minimumVerticalSpeed)
        {
            float verticalDirection;

            if (Mathf.Approximately(velocity.y, 0f))
            {
                verticalDirection = Random.value < 0.5f ? -1f : 1f;
            }
            else
            {
                verticalDirection = Mathf.Sign(velocity.y);
            }

            velocity.y = minimumVerticalSpeed * verticalDirection;
        }

        // Prevent nearly vertical movement.
        if (Mathf.Abs(velocity.x) < minimumHorizontalSpeed)
        {
            float horizontalDirection;

            if (Mathf.Approximately(velocity.x, 0f))
            {
                horizontalDirection = Random.value < 0.5f ? -1f : 1f;
            }
            else
            {
                horizontalDirection = Mathf.Sign(velocity.x);
            }

            velocity.x = minimumHorizontalSpeed * horizontalDirection;
        }

        // Keep the overall speed constant.
        rb.linearVelocity = velocity.normalized * speed;
    }

    public void ResetBall()
    {
        StopAllCoroutines();

        float randomX = Random.Range(minSpawnX, maxSpawnX);
        rb.position = new Vector2(randomX, spawnY);
        rb.linearVelocity = Vector2.zero;

        StartCoroutine(DelayedLaunchBall());
    }

}
