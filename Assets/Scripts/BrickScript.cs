using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points = 100;
    private ScoreScript scoreScript;
    private RewardScript rewardScript;
    public GameObject BrickBreakEffect;
    public AudioClip brickBreak;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreScript = FindObjectOfType<ScoreScript>();
        rewardScript = FindFirstObjectByType<RewardScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        scoreScript.UpdateScore(points);
        rewardScript.BrickBrokenReward();
        AudioSource.PlayClipAtPoint(brickBreak, transform.position);
        Destroy(gameObject);
        Debug.Log("Brick Destroyed");
        Instantiate(BrickBreakEffect, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
