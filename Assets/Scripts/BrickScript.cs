using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points = 100;
    private ScoreScript scoreScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreScript = FindObjectOfType<ScoreScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        scoreScript.UpdateScore(points);
        Destroy(gameObject);
        Debug.Log("Brick Destroyed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
