using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points = 100;
    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private RewardScript rewardScript;
    public GameObject BrickBreakEffect;
    public AudioClip brickBreak;
    public BrickSpawnScript brickSpawnScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        brickSpawnScript = GetComponentInParent<BrickSpawnScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        scoreScript.UpdateScore(points);
        AudioSource.PlayClipAtPoint(brickBreak, transform.position);
        brickSpawnScript.BricksDestroyed();
        Destroy(gameObject);
        Debug.Log("Brick Destroyed");
        Instantiate(BrickBreakEffect, transform.position, transform.rotation);
        if (rewardScript != null)
        {
            rewardScript.BrickBrokenReward();
        }
    }

    public void SetScoreScript(ScoreScript script)
    {
        scoreScript = script;
    }

    public void SetRewardScript(RewardScript script)
    {
        rewardScript = script;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
