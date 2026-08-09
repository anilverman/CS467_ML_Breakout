using System.Collections;
using UnityEngine;

public class BrickSpawnScript : MonoBehaviour
{
    public GameObject Brick;
    public int rows = 6;
    public int columns = 6;
    public float rowSpace = 0.25f;
    public float columnSpace = 0.25f;
    public Vector2 startPosition = new Vector2(-10, 5);
    public Color[] colors;
    private int bricksLeft;
    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private RewardScript rewardScript;
    [SerializeField] private BallScript ballScript;

    void Start()
    {
        ballScript = FindFirstObjectByType<BallScript>();
        SpawnBricks();
    }

    /// <summary>
    /// Destroys all existing bricks and spawns a fresh set.
    /// </summary>
    public void ResetBricks()
    {
        StartCoroutine(ResetBricksCoroutine());
    }

    private IEnumerator ResetBricksCoroutine()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Wait one frame so the old bricks are actually destroyed.
        yield return null;

        SpawnBricks();
        // Comment this out when training.
        ballScript.speed ++;
        Debug.Log(ballScript.speed);
    }

    /// <summary>
    /// Spawns bricks in uniform rows and columns.
    /// Each row gets a random color with no repeated row colors.
    /// </summary>
    public void SpawnBricks()
    {
        bricksLeft = rows * columns;
        Color[] rowColors = new Color[rows];

        // Choose a unique color for each row.
        for (int i = 0; i < rows; i++)
        {
            Color randomColor;

            do
            {
                randomColor = colors[Random.Range(0, colors.Length)];
            }
            while (System.Array.Exists(rowColors, c => c == randomColor));

            rowColors[i] = randomColor;
        }

        // Spawn the bricks.
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(
                    startPosition.x + (col * columnSpace),
                    startPosition.y - (row * rowSpace)
                );

                GameObject newBrick = Instantiate(
                    Brick,
                    position,
                    Quaternion.identity,
                    transform   // Make this spawner the parent
                );

                SpriteRenderer renderer = newBrick.GetComponent<SpriteRenderer>();
                renderer.color = rowColors[row];
                newBrick.GetComponent<BrickScript>().SetScoreScript(scoreScript);
                newBrick.GetComponent<BrickScript>().SetRewardScript(rewardScript);
            }
        }
    }
    public void BricksDestroyed()
    {
        bricksLeft --;

        if (bricksLeft <= 0)
        {   
            ResetBricks();
            ballScript.ResetBall();
        }
    }
}
