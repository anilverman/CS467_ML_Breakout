using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.InputSystem;

public class PaddleAgent : Agent
{
    [Header("References")]
    [SerializeField] private Transform ball;
    [SerializeField] private Rigidbody2D ballRigidbody;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float minX = -7f;
    [SerializeField] private float maxX = 7f;

    private Rigidbody2D paddleRigidbody;
    private Vector3 paddleStartPosition;
    private BallScript ballScript;
    private BrickSpawnScript brickScript;
    private RewardScript rewardScript;

    public override void Initialize()
    {
        paddleRigidbody = GetComponent<Rigidbody2D>();
        paddleStartPosition = transform.position;

        ballScript = ball.GetComponent<BallScript>();
        brickScript = FindObjectOfType<BrickSpawnScript>();
        rewardScript = FindFirstObjectByType<RewardScript>();
    }

    public override void OnEpisodeBegin()
    {
        // restart the paddle position and reset its velocity
        transform.position = paddleStartPosition;

        if (paddleRigidbody != null)
        {
            paddleRigidbody.linearVelocity = Vector2.zero;
            paddleRigidbody.angularVelocity = 0f;
        }

        // reset the ball and respawn bricks when a new episode begins
        ballScript.ResetBall();
        brickScript.ResetBricks();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Paddle horizontal position.
        sensor.AddObservation(transform.position.x);

        // Ball position relative to the paddle.
        Vector3 relativeBallPosition = ball.position - transform.position;
        sensor.AddObservation(relativeBallPosition.x);
        sensor.AddObservation(relativeBallPosition.y);

        // Ball movement.
        sensor.AddObservation(ballRigidbody.linearVelocity.x);
        sensor.AddObservation(ballRigidbody.linearVelocity.y);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // One continuous action:
        // -1 = move left
        //  0 = do not move
        //  1 = move right
        float direction = Mathf.Clamp(
            actionBuffers.ContinuousActions[0],
            -1f,
            1f
        );

        float newX =
            transform.position.x +
            direction * moveSpeed * Time.fixedDeltaTime;

        newX = Mathf.Clamp(newX, minX, maxX);

        Vector2 newPosition = new Vector2(
            newX,
            transform.position.y
        );

        paddleRigidbody.MovePosition(newPosition);

        // Tiny reward for staying alive.
        rewardScript.AliveReward();
    }

    // If the ball falls below the paddle, end the episode with a negative reward. Called from BallScript when the ball goes out of bounds.
    public void HandleBallOutOfBounds()
    {
        rewardScript.LostBallPenalty();
        EndEpisode();
        Debug.Log("Episode ended due to ball going out of bounds.");
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActions = actionsOut.ContinuousActions;
        float direction = 0f;

        if (Keyboard.current.leftArrowKey.isPressed ||
            Keyboard.current.aKey.isPressed)
        {
            direction = -1f;
        }

        if (Keyboard.current.rightArrowKey.isPressed ||
            Keyboard.current.dKey.isPressed)
        {
            direction = 1f;
        }

        continuousActions[0] = direction;
    }
}
