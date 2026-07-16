using UnityEngine;

public class BrickSpawnScript : MonoBehaviour
{
    public GameObject Brick;
    public int rows = 6;
    public int columns = 6;
    public float rowSpace =.25f;
    public float columnSpace = .25f;
    public Vector2 startPosition = new Vector2(-10, 5);
    public Color[] colors;

    void Start()
    {
        SpawnBricks();
    }
/// <summary>
/// Spawns bricks in uniform rows and columns. Randomizes the colors & does not allow repeat colors.
/// </summary>
    public void SpawnBricks()
{
    Color[] rowColors = new Color[rows];

    // Checks to make sure a color isn't used then assigns the color.
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

    // Generates each brick and places them in a row and column. Number of rows and columns editable on Unity.
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < columns; col++)
        {
            Vector2 position = new Vector2(
                startPosition.x + (col * columnSpace),
                startPosition.y - (row * rowSpace)
            );

            GameObject newBrick = Instantiate(Brick, position, Quaternion.identity);

            SpriteRenderer renderer = newBrick.GetComponent<SpriteRenderer>();
            renderer.color = rowColors[row];
        }
    }
}
}
