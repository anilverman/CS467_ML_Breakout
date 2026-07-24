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
        scoreScript = FindObjectOfType<ScoreScript>();
        rewardScript = FindFirstObjectByType<RewardScript>();
        brickSpawnScript = FindFirstObjectByType<BrickSpawnScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        scoreScript.UpdateScore(points);
        AudioSource.PlayClipAtPoint(brickBreak, transform.position);
        brickSpawnScript.BricksDestroyed();
        Destroy(gameObject);
        Debug.Log("Brick Destroyed");
        Instantiate(BrickBreakEffect, transform.position, transform.rotation);
        rewardScript.BrickBrokenReward();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
