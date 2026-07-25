using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points = 100;
    private ScoreScript scoreScript;
    private RewardScript rewardScript;
    public GameObject BrickBreakEffect;
    public AudioClip brickBreak;
    public BrickSpawnScript brickSpawnScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoardContext board = GetComponentInParent<BoardContext>();
        scoreScript = board != null ? board.score : FindObjectOfType<ScoreScript>();
        rewardScript = board != null ? board.rewards : FindFirstObjectByType<RewardScript>();
        brickSpawnScript = board != null ? board.brickSpawner : FindFirstObjectByType<BrickSpawnScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (scoreScript != null)
        {
            scoreScript.UpdateScore(points);
        }
        AudioSource.PlayClipAtPoint(brickBreak, transform.position);
        brickSpawnScript.BricksDestroyed();
        Destroy(gameObject);
        Debug.Log("Brick Destroyed");
        Instantiate(BrickBreakEffect, transform.position, transform.rotation);
        if (rewardScript != null && boardAgentIsTraining())
        {
            rewardScript.BrickBrokenReward();
        }
    }

    private bool boardAgentIsTraining()
    {
        BoardContext board = GetComponentInParent<BoardContext>();
        return board != null && board.agent != null && board.agent.isTrainingSession;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
