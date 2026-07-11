using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points = 100;
    public ScoreScript scoreScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.CompareTag("Ball")){
            scoreScript.UpdateScore(points);
            Destroy(gameObject);
            Debug.Log("Brick Destroyed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
